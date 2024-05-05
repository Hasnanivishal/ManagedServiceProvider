using Microsoft.AspNetCore.Mvc;
using MSP.Profile.Model;
using MSP.Profile.Repository;
using MSP.Profile.SyncCommunication.gRPC;
using MSP.Profile.SyncCommunication.Http;
using StackExchange.Redis;
using System.Text.Json;

namespace MSP.Profile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController(IMongoDbContext<ProfileEntity> mongoDbContext,
        IHttpCommunicationClient httpCommunicationClient,
        IGrpcCommunicationService grpcCommunicationService,
        IConnectionMultiplexer muxer) : ControllerBase
    {
        private readonly IMongoDbContext<ProfileEntity> mongoDbContext = mongoDbContext;
        private readonly IHttpCommunicationClient httpCommunicationClient = httpCommunicationClient;
        private readonly IGrpcCommunicationService grpcCommunicationService = grpcCommunicationService;
        private readonly IDatabase _redis = muxer.GetDatabase();

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProfileDto>>> Get()
        {
            var result = (await mongoDbContext.GetAllAsync()).Select(item => item.AsProfileDto());

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProfileDto>> GetById(Guid id)
        {
            // Define a unique key based on profile id
            string keyName = $"ProfileDettailsOf:{id}";

            // Get data from Redis
            string json = await _redis.StringGetAsync(keyName);

            if (string.IsNullOrEmpty(json))
            {
                var item = await mongoDbContext.GetAsync(id);

                if (item is null)
                {
                    return NotFound();
                }

                json = JsonSerializer.Serialize(item);

                var setTask = _redis.StringSetAsync(keyName, json);
                var expireTask = _redis.KeyExpireAsync(keyName, TimeSpan.FromSeconds(300));

                await Task.WhenAll(setTask, expireTask);
            }

            return JsonSerializer.Deserialize<ProfileEntity>(json)!.AsProfileDto();
        }

        [HttpGet("GetFullProfile/{id}")]
        public async Task<ActionResult<ProfileDto>> GetFullProfile(Guid id)
        {
            var item = await mongoDbContext.GetAsync(id);

            if (item is null)
            {
                return NotFound();
            }

            // Make an HTTP call to Subscription Service
            var subscriptionDetails = await httpCommunicationClient.GetDataOverHttp<SubscriptionResult>("SubscritonService", $"GetById/{id}");

            // GRPC call to fetch coupons for this user
            var couponDetails = grpcCommunicationService.GetCouponDetails(id).AsCouponResult();

            return item.AsProfileDto(subscriptionDetails, couponDetails);
        }

        [HttpPost]
        public async Task<ActionResult<ProfileDto>> Post([FromBody] AddProfileDto addProfile)
        {
            var profileEntity = new ProfileEntity
            {
                Id = Guid.NewGuid(),
                Name = addProfile.Name
            };

            await mongoDbContext.CreateAsync(profileEntity);

            return CreatedAtAction(nameof(GetById), new { id = profileEntity.Id }, profileEntity.AsProfileDto());
        }

        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
