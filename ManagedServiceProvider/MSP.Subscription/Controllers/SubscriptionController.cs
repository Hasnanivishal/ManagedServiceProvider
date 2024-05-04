using Microsoft.AspNetCore.Mvc;
using MSP.Subscription.Model;
using MSP.Subscription.Repository;

namespace MSP.Subscription.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController(AppDbContext appDbContext) : ControllerBase
    {
        private readonly AppDbContext appDbContext = appDbContext;

        [HttpGet("GetById/{profileId}")]
        public IEnumerable<SubscriptionDetails> GetById([FromRoute] Guid profileId)
        {
            var data = appDbContext.Subscription.Where(s => s.ProfileId == profileId)
                .Select(item => item.AsSubscriptionDetails()).ToList();

            return data;
        }


        [HttpPost]
        public ActionResult<SubscriptionDetails> Post([FromBody] AddSubscriptionDto addSubscription)
        {
            var subscriptionEntity = new SubscriptionEntity
            {
                Id = Guid.NewGuid(),
                IsActive = true,
                Provider = addSubscription.ProviderName,
                ProfileId = addSubscription.ProfileId,
                ExpiryDate = DateTime.Today.AddDays(30)
            };

            appDbContext.Subscription.Add(subscriptionEntity);
            appDbContext.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { profileId = subscriptionEntity.ProfileId }, subscriptionEntity.AsSubscriptionDetails());
        }
    }
}
