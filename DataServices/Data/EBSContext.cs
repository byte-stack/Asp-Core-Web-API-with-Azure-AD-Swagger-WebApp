using WebAPI.DataServices.Table;
using Microsoft.EntityFrameworkCore;


namespace WebAPI.DataServices.Data
{
    /// <summary>
    /// DB context class for binding the DB objects
    /// </summary>
    public class EBSContext : DbContext
    {
        public EBSContext(DbContextOptions<EBSContext> options) : base(options) { }

        /// <summary>
        /// User Db set. Access to user db table
        /// </summary>
        public DbSet<Users> User { get; set; }

        /// <summary>
        /// BillingCycle  db set. Access to BillingCycle db table.
        /// </summary>
        public DbSet<BillingCycle> BillingCycle { get; set; }

        /// <summary>
        /// BillingDetails db set. Access to BillingDetails db table.
        /// </summary>
        public DbSet<BillingDetails> BillingDetails { get; set; }

        /// <summary>
        /// UserProfile Db set. Access to UserProfile db table.
        /// </summary>
        public DbSet<UserProfile> UserProfile { get; set; }

        /// <summary>
        /// Db table model override to map to db table
        /// </summary>
        /// <param name="modelBuilder"> Model builder class object</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>().HasKey(c => c.Id);
            modelBuilder.Entity<BillingCycle>().HasKey(c => c.Id);
            modelBuilder.Entity<BillingDetails>().HasKey(c => c.Id);
            modelBuilder.Entity<UserProfile>().HasKey(c => c.Id);
        }
    }
}
