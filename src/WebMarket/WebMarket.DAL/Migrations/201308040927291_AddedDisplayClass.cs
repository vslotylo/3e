using System.Data.Entity.Migrations;

namespace WebMarket.DAL.Migrations
{
    public partial class AddedDisplayClass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Avrs", "DisplayClass", c => c.Int(nullable: false));
            AddColumn("dbo.Ups", "DisplayClass", c => c.Int(nullable: false));
            AddColumn("dbo.Converters", "DisplayClass", c => c.Int(nullable: false));
            AddColumn("dbo.Batteries", "DisplayClass", c => c.Int(nullable: false));
            AddColumn("dbo.Chargers", "DisplayClass", c => c.Int(nullable: false));
            AddColumn("dbo.VoltageRelays", "DisplayClass", c => c.Int(nullable: false));
            AddColumn("dbo.CurrentRelays", "DisplayClass", c => c.Int(nullable: false));
            AddColumn("dbo.TemperatureRelays", "DisplayClass", c => c.Int(nullable: false));
            AddColumn("dbo.TimeRelays", "DisplayClass", c => c.Int(nullable: false));
            AddColumn("dbo.Voltmeters", "DisplayClass", c => c.Int(nullable: false));
            DropColumn("dbo.Avrs", "IsTopBuyed");
            DropColumn("dbo.Avrs", "IsNew");
            DropColumn("dbo.Ups", "IsTopBuyed");
            DropColumn("dbo.Ups", "IsNew");
            DropColumn("dbo.Converters", "IsTopBuyed");
            DropColumn("dbo.Converters", "IsNew");
            DropColumn("dbo.Batteries", "IsTopBuyed");
            DropColumn("dbo.Batteries", "IsNew");
            DropColumn("dbo.Chargers", "IsTopBuyed");
            DropColumn("dbo.Chargers", "IsNew");
            DropColumn("dbo.VoltageRelays", "IsTopBuyed");
            DropColumn("dbo.VoltageRelays", "IsNew");
            DropColumn("dbo.CurrentRelays", "IsTopBuyed");
            DropColumn("dbo.CurrentRelays", "IsNew");
            DropColumn("dbo.TemperatureRelays", "IsTopBuyed");
            DropColumn("dbo.TemperatureRelays", "IsNew");
            DropColumn("dbo.TimeRelays", "IsTopBuyed");
            DropColumn("dbo.TimeRelays", "IsNew");
            DropColumn("dbo.Voltmeters", "IsTopBuyed");
            DropColumn("dbo.Voltmeters", "IsNew");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Voltmeters", "IsNew", c => c.Boolean(nullable: false));
            AddColumn("dbo.Voltmeters", "IsTopBuyed", c => c.Boolean(nullable: false));
            AddColumn("dbo.TimeRelays", "IsNew", c => c.Boolean(nullable: false));
            AddColumn("dbo.TimeRelays", "IsTopBuyed", c => c.Boolean(nullable: false));
            AddColumn("dbo.TemperatureRelays", "IsNew", c => c.Boolean(nullable: false));
            AddColumn("dbo.TemperatureRelays", "IsTopBuyed", c => c.Boolean(nullable: false));
            AddColumn("dbo.CurrentRelays", "IsNew", c => c.Boolean(nullable: false));
            AddColumn("dbo.CurrentRelays", "IsTopBuyed", c => c.Boolean(nullable: false));
            AddColumn("dbo.VoltageRelays", "IsNew", c => c.Boolean(nullable: false));
            AddColumn("dbo.VoltageRelays", "IsTopBuyed", c => c.Boolean(nullable: false));
            AddColumn("dbo.Chargers", "IsNew", c => c.Boolean(nullable: false));
            AddColumn("dbo.Chargers", "IsTopBuyed", c => c.Boolean(nullable: false));
            AddColumn("dbo.Batteries", "IsNew", c => c.Boolean(nullable: false));
            AddColumn("dbo.Batteries", "IsTopBuyed", c => c.Boolean(nullable: false));
            AddColumn("dbo.Converters", "IsNew", c => c.Boolean(nullable: false));
            AddColumn("dbo.Converters", "IsTopBuyed", c => c.Boolean(nullable: false));
            AddColumn("dbo.Ups", "IsNew", c => c.Boolean(nullable: false));
            AddColumn("dbo.Ups", "IsTopBuyed", c => c.Boolean(nullable: false));
            AddColumn("dbo.Avrs", "IsNew", c => c.Boolean(nullable: false));
            AddColumn("dbo.Avrs", "IsTopBuyed", c => c.Boolean(nullable: false));
            DropColumn("dbo.Voltmeters", "DisplayClass");
            DropColumn("dbo.TimeRelays", "DisplayClass");
            DropColumn("dbo.TemperatureRelays", "DisplayClass");
            DropColumn("dbo.CurrentRelays", "DisplayClass");
            DropColumn("dbo.VoltageRelays", "DisplayClass");
            DropColumn("dbo.Chargers", "DisplayClass");
            DropColumn("dbo.Batteries", "DisplayClass");
            DropColumn("dbo.Converters", "DisplayClass");
            DropColumn("dbo.Ups", "DisplayClass");
            DropColumn("dbo.Avrs", "DisplayClass");
        }
    }
}
