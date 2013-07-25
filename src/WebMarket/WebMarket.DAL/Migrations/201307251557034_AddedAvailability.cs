namespace WebMarket.DAL.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddedAvailability : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Avrs", "Availability", c => c.Int(nullable: false));
            AddColumn("dbo.Ups", "Availability", c => c.Int(nullable: false));
            AddColumn("dbo.Converters", "Availability", c => c.Int(nullable: false));
            AddColumn("dbo.Batteries", "Availability", c => c.Int(nullable: false));
            AddColumn("dbo.Chargers", "Availability", c => c.Int(nullable: false));
            AddColumn("dbo.VoltageRelays", "Availability", c => c.Int(nullable: false));
            AddColumn("dbo.CurrentRelays", "Availability", c => c.Int(nullable: false));
            AddColumn("dbo.TemperatureRelays", "Availability", c => c.Int(nullable: false));
            AddColumn("dbo.TimeRelays", "Availability", c => c.Int(nullable: false));
            AddColumn("dbo.Voltmeters", "Availability", c => c.Int(nullable: false));
            DropColumn("dbo.Avrs", "IsAvailable");
            DropColumn("dbo.Ups", "IsAvailable");
            DropColumn("dbo.Converters", "IsAvailable");
            DropColumn("dbo.Batteries", "IsAvailable");
            DropColumn("dbo.Chargers", "IsAvailable");
            DropColumn("dbo.VoltageRelays", "IsAvailable");
            DropColumn("dbo.CurrentRelays", "IsAvailable");
            DropColumn("dbo.TemperatureRelays", "IsAvailable");
            DropColumn("dbo.TimeRelays", "IsAvailable");
            DropColumn("dbo.Voltmeters", "IsAvailable");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Voltmeters", "IsAvailable", c => c.Boolean(nullable: false));
            AddColumn("dbo.TimeRelays", "IsAvailable", c => c.Boolean(nullable: false));
            AddColumn("dbo.TemperatureRelays", "IsAvailable", c => c.Boolean(nullable: false));
            AddColumn("dbo.CurrentRelays", "IsAvailable", c => c.Boolean(nullable: false));
            AddColumn("dbo.VoltageRelays", "IsAvailable", c => c.Boolean(nullable: false));
            AddColumn("dbo.Chargers", "IsAvailable", c => c.Boolean(nullable: false));
            AddColumn("dbo.Batteries", "IsAvailable", c => c.Boolean(nullable: false));
            AddColumn("dbo.Converters", "IsAvailable", c => c.Boolean(nullable: false));
            AddColumn("dbo.Ups", "IsAvailable", c => c.Boolean(nullable: false));
            AddColumn("dbo.Avrs", "IsAvailable", c => c.Boolean(nullable: false));
            DropColumn("dbo.Voltmeters", "Availability");
            DropColumn("dbo.TimeRelays", "Availability");
            DropColumn("dbo.TemperatureRelays", "Availability");
            DropColumn("dbo.CurrentRelays", "Availability");
            DropColumn("dbo.VoltageRelays", "Availability");
            DropColumn("dbo.Chargers", "Availability");
            DropColumn("dbo.Batteries", "Availability");
            DropColumn("dbo.Converters", "Availability");
            DropColumn("dbo.Ups", "Availability");
            DropColumn("dbo.Avrs", "Availability");
        }
    }
}
