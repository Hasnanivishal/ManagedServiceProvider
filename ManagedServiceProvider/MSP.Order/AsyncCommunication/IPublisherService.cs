using MSP.Order.Model;

namespace MSP.Order.AsyncCommunication
{
    public interface IPublisherService
    {
        void PublishOrderCreated(OrderEntity orderEntity);
    }
}