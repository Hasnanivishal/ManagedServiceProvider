namespace MSP.Profile.Model
{
    public class SubscriptionResult
    {
        public string Provider { get; set; }

        public bool IsActive { get; set; }

        public DateTime? ExpiryDate { get; set; }
    }
}
