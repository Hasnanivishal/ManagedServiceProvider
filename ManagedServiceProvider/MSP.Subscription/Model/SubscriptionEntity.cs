using System.ComponentModel.DataAnnotations;

namespace MSP.Subscription.Model
{
    public class SubscriptionEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public required string Provider { get; set; }

        [Required]
        public required bool IsActive { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public Guid ProfileId { get; set; }
    }
}
