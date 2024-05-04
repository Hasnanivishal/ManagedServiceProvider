namespace MSP.Subscription.Model;

public class SubscriptionDetails
{
    public string ProviderName { get; set; }

    public bool IsActive { get; set; }

    public DateTime? ExpiryDate { get; set; }
}

public record AddSubscriptionDto(string ProviderName, Guid ProfileId);

public static class Extensions
{
    public static SubscriptionDetails AsSubscriptionDetails(this SubscriptionEntity subscriptionEntity)
    {
        return new SubscriptionDetails()
        {
            ProviderName = subscriptionEntity.Provider,
            ExpiryDate = subscriptionEntity.ExpiryDate,
            IsActive = subscriptionEntity.IsActive
        };
    }
}
