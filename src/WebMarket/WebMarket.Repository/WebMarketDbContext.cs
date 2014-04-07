using System.Data.Entity;
using WebMarket.Repository.Entities;
using Configuration = WebMarket.Repository.Migrations.Configuration;

namespace WebMarket.Repository
{
    public class WebMarketDbContext : DbContext
    {
        public WebMarketDbContext():
            base("DefaultConnection")
        {
            
        }

        public WebMarketDbContext(string connectionString)
            : base(connectionString)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<WebMarketDbContext, Configuration>());
            base.OnModelCreating(modelBuilder);
        }
        
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Callback> Callbacks { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Group> Groups { get; set; }
    }
}
