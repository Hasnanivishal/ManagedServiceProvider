using Microsoft.AspNetCore.Mvc;
using MSP.Profile.Model;
using MSP.Profile.Repository;
using MSP.Profile.SyncCommunication.gRPC;
using MSP.Profile.SyncCommunication.Http;

namespace MSP.Profile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController(IMongoDbContext<ProfileEntity> mongoDbContext,
        IHttpCommunicationClient httpCommunicationClient,
        IGrpcCommunicationService grpcCommunicationService) : ControllerBase
    {
        private readonly IMongoDbContext<ProfileEntity> mongoDbContext = mongoDbContext;

        private readonly IHttpCommunicationClient httpCommunicationClient = httpCommunicationClient;
        private readonly IGrpcCommunicationService grpcCommunicationService = grpcCommunicationService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProfileDto>>> Get()
        {
            var result = (await mongoDbContext.GetAllAsync()).Select(item => item.AsProfileDto());

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProfileDto>> GetById(Guid id)
        {
            var item = await mongoDbContext.GetAsync(id);

            if (item is null)
            {
                return NotFound();
            }

            return item.AsProfileDto();
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

            return CreatedAtAction(nameof(GetById), new { id = profileEntity.Id }, profileEntity);
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
