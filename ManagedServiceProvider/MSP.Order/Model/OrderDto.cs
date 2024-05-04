namespace MSP.Order.Model;

public record OrderDto(Guid Id, string Name, decimal Amount);

public record AddOrderDto(string Name, decimal Amount);

public static class Extensions
{
    public static OrderDto AsOrderDto(this OrderEntity orderEntity)
    {
        return new OrderDto(orderEntity.Id, orderEntity.Name, orderEntity.Amount);
    }
}

