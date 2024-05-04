
using MassTransit;
using MSP.Common.Model;
using MSP.Subscription.Model;
using MSP.Subscription.Repository;

namespace MSP.Subscription.Service
{
    public class OrderNotificationConsumer(AppDbContext appDbContext) : IConsumer<OrderNotification>
    {
        private readonly AppDbContext appDbContext = appDbContext;

        public Task Consume(ConsumeContext<OrderNotification> context)
        {
            var orderNotification = context.Message;

            var subscriptionEntity = new SubscriptionEntity
            {
                Id = Guid.NewGuid(),
                IsActive = true,
                Provider = orderNotification.Name,
                ProfileId = orderNotification.ProfileId,
                ExpiryDate = DateTime.Today.AddDays(30)
            };

            appDbContext.Subscription.Add(subscriptionEntity);
            appDbContext.SaveChanges();

            return Task.CompletedTask;
        }
    }
}
