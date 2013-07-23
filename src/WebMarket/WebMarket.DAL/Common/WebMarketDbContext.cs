﻿using System.Configuration;
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
        public DbSet<Callback> Callbacks { get; set; }
        //public DbSet<Product> Products { get; set; }
    }
}