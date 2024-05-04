using MassTransit;
using MSP.Common.Model;

namespace MSP.Coupon.Service;

public class OrderNotificationConsumer() : IConsumer<OrderNotification>
{
    public Task Consume(ConsumeContext<OrderNotification> context)
    {
        Console.WriteLine($"Received Message as...{context.Message.Name}");

        return Task.CompletedTask;
    }
}
