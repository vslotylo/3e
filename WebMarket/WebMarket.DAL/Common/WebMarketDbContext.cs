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
            //modelBuilder.Entity<Avr>().ToTable("Avrs");
            //modelBuilder.Entity<Battery>().ToTable("Batteries");
            //modelBuilder.Entity<Charger>().ToTable("Chargers");
            //modelBuilder.Entity<Converter>().ToTable("Converters");
            //modelBuilder.Entity<Regulator>().ToTable("Regulators");
            //modelBuilder.Entity<Ups>().ToTable("Upses");

            //modelBuilder.Entity<Product>().Ignore(t => t.BuyCurrency);
            //modelBuilder.Entity<Product>().Ignore(t => t.PriceUah);
            //modelBuilder.Entity<Product>().Ignore(t => t.PriceUsd);
            // modelBuilder.Entity<Product>().Ignore(t => t.ProductID);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Avr> Avrs { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Ups> Ups { get; set; }
        public DbSet<Converter> Converters { get; set; }
        public DbSet<Battery> Batteries { get; set; }
        public DbSet<Charger> Chargers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<VoltageRelay> VoltageRelays { get; set; }
        public DbSet<CurrentRelay> CurrentRelays { get; set; }
        public DbSet<TemperatureRelay> TemperatureRelays { get; set; }
        public DbSet<TimeRelay> TimeRelays { get; set; }
        public DbSet<Voltmeter> Voltmeters { get; set; }
        //public DbSet<Product> Products { get; set; }
    }
}
