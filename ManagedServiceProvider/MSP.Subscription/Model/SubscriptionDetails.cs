namespace MSP.Subscription.Model
{
    public class SubscriptionDetails
    {
        public Guid Id { get; set; }

        public string Provider { get; set; }

        public bool IsActive { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public Guid ProfileId { get; set; }
    }
}
