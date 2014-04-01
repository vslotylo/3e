using System.Configuration;
using System.Data.Entity;
using WebMarket.DAL.Entities;

namespace WebMarket.DAL.Common
{
    public class WebMarketDbContext : DbContext
    {
        public WebMarketDbContext()
            : base(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<WebMarketDbContext, Migrations.Configuration>());
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
