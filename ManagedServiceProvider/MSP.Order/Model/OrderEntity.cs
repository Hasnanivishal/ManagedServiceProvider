namespace MSP.Order.Model;

public class OrderEntity
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public decimal Amount { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    public Guid ProfileId { get; set; }
}
