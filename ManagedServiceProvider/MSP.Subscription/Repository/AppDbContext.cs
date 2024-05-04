using Microsoft.EntityFrameworkCore;
using MSP.Subscription.Model;

namespace MSP.Subscription.Repository
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<SubscriptionEntity> Subscription { get; set; }
    }
}
