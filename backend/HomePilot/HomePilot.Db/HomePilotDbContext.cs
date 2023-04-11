using HomePilot.Db.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace HomePilot.Db
{
    public class HomePilotDbContext : DbContext
    {
        private const string HomePilotDbConnectionString = "Database=HomePilot; Data Source=localhost; Port=3333; User Id=root; Password=Coucou42";

        // https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli
        public class HomePilotDbContextDesignFactory : IDesignTimeDbContextFactory<HomePilotDbContext>
        {
            public HomePilotDbContext CreateDbContext(string[] args)
            {
                DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder<HomePilotDbContext>();

                optionsBuilder.UseMySql(
                    HomePilotDbConnectionString,
                    new MariaDbServerVersion(new Version(10, 6)),
                    options =>
                    {
                        /*
                         * PRED-2032 otherwise we have a lot of 
                         * "
                         * An exception has been raised that is likely due to a transient failure.
                         * Consider enabling transient error resiliency by adding 'EnableRetryOnFailure()' to the 'UseMySql' call.
                         * Connect Timeout expired.
                         * Unable to connect to any of the specified MySQL hosts.
                         * "
                         * errors in prod
                         */
                        options.EnableRetryOnFailure();
                        options.CommandTimeout(5) /* PRED-2328 */;
                    }
                );

                return new HomePilotDbContext(optionsBuilder.Options);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LeaseTenantModel>()
                .HasKey(nameof(LeaseTenantModel.LeaseId), nameof(LeaseTenantModel.TenantId));
        }

        public HomePilotDbContext(DbContextOptions options) : base(options) { }

        public virtual DbSet<LeaseModel> Leases { get; set; } = null!;

        public virtual DbSet<TenantModel> Tenants { get; set; } = null!;

        public virtual DbSet<LeaseTenantModel> LeaseTenants { get; set; } = null!;

    }
}