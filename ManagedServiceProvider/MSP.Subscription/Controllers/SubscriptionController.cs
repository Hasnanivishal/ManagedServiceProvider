using Microsoft.AspNetCore.Mvc;
using MSP.Subscription.Model;

namespace MSP.Subscription.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {

        [HttpGet("GetById/{profileId}")]
        public IEnumerable<SubscriptionDetails> Get([FromRoute] Guid profileId)
        {
            return
            [
                new SubscriptionDetails()
                {
                    Id = Guid.NewGuid(),
                    IsActive = true,
                    ProfileId = profileId,
                    Provider = "Disney+Hotstar",
                    ExpiryDate = DateTime.Today.AddDays(290).Date
                },
                new SubscriptionDetails()
                {
                    Id = Guid.NewGuid(),
                    IsActive = true,
                    ProfileId = profileId,
                    Provider = "Netflix",
                    ExpiryDate = DateTime.Today.AddDays(30).Date
                },
                new SubscriptionDetails()
                {
                    Id = Guid.NewGuid(),
                    IsActive = false,
                    ProfileId = profileId,
                    Provider = "AmazonPrime",
                    ExpiryDate = null
                }
            ];
        }
    }
}
