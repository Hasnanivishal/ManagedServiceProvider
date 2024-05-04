using MassTransit;
using MSP.Common.Model;
using MSP.Order.Model;

namespace MSP.Order.AsyncCommunication
{
    public class PublisherService(IPublishEndpoint publishEndpoint) : IPublisherService
    {
        private readonly IPublishEndpoint publishEndpoint = publishEndpoint;

        public async void PublishOrderCreated(OrderEntity orderEntity)
        {
            var orderNotification = new OrderNotification()
            {
                Id = orderEntity.Id,
                Name = orderEntity.Name,
                ProfileId = orderEntity.ProfileId,
            };

            await publishEndpoint.Publish(orderNotification);
        }
    }
}
