namespace MSP.Profile.Model;

public record ProfileDto(Guid Id, string Name)
{
    public IEnumerable<SubscriptionResult>? Subscriptions { get; set; }
    public IEnumerable<CouponResult>? Coupons { get; set; }
};

public record AddProfileDto(string Name);

public static class Extensions
{
    public static ProfileDto AsProfileDto(this ProfileEntity profileEntity,
        IEnumerable<SubscriptionResult>? subscriptions = null,
        IEnumerable<CouponResult>? coupons = null)
    {
        return new ProfileDto(profileEntity.Id, profileEntity.Name)
        {
            Subscriptions = subscriptions,
            Coupons = coupons
        };
    }
}

