using DealMeCore.DB.Infrastructure.EntitiesConfiguration;
using DealMeCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DealMeCore.DB.Infrastructure
{
    /// <summary>
    /// DealMeCore database context.
    /// </summary>
    public class DealMeCoreDbContext : DbContext
    {
        private const string connectionString = "Server=.;Database=DealMeCore;Trusted_Connection=True;";

        public DealMeCoreDbContext(DbContextOptions<DealMeCoreDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString, x => x.UseNetTopologySuite());
        }

        #region Db Sets

        public DbSet<Brand> Brands { get; set; }

        public DbSet<Store> Stores { get; set; }

        public DbSet<Deal> Deals { get; set; }

        public DbSet<DealImage> DealImages { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ConfigureTables(modelBuilder);
        }

        private void ConfigureTables(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BrandConfiguration());
            modelBuilder.ApplyConfiguration(new StoreConfiguration());
            modelBuilder.ApplyConfiguration(new DealConfiguration());
            modelBuilder.ApplyConfiguration(new DealImageConfiguration());
        }
    }
}
