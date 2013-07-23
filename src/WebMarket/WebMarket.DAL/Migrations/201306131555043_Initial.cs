using System.Data.Entity.Migrations;
namespace WebMarket.DAL.Migrations
{
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Avrs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MinWorkingInput = c.Double(nullable: false),
                        MaxWorkingInput = c.Double(nullable: false),
                        Precision = c.String(),
                        MinCutOff = c.Double(nullable: false),
                        MaxCutOff = c.Double(nullable: false),
                        Stages = c.Int(nullable: false),
                        ResponseTime = c.Int(nullable: false),
                        HasDisplay = c.Boolean(),
                        HasModeDisplay = c.Boolean(),
                        HasInputOutputDisplay = c.Boolean(),
                        HasHighInputVoltageProtection = c.Boolean(),
                        HasHighOutputVoltageProtection = c.Boolean(),
                        HasOverloadProtection = c.Boolean(),
                        HasOverheatProtection = c.Boolean(),
                        HasShortCircuitProtection = c.Boolean(),
                        HasMemoryCard = c.Boolean(),
                        StartDelay = c.Int(nullable: false),
                        PowerEfficiency = c.Int(nullable: false),
                        Efficiency = c.String(),
                        SelfConsumption = c.String(),
                        ProtectionLevel = c.String(),
                        CabinetType = c.String(),
                        PowerCapacity = c.Double(nullable: false),
                        PhaseCount = c.Int(nullable: false),
                        InputRange = c.String(),
                        Name = c.String(),
                        Price = c.Double(nullable: false),
                        Weight = c.Double(nullable: false),
                        Rate = c.Double(nullable: false),
                        Photo = c.String(),
                        Description = c.String(),
                        WorkingConditions = c.String(),
                        Dimension = c.String(),
                        Warranty = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        IsAvailable = c.Boolean(nullable: false),
                        IsTopBuyed = c.Boolean(nullable: false),
                        IsNew = c.Boolean(nullable: false),
                        Discount = c.Double(nullable: false),
                        Info = c.String(),
                        SuppliedItems = c.String(),
                        Producer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Producers", t => t.Producer_Id)
                .Index(t => t.Producer_Id);
            
            CreateTable(
                "dbo.Producers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        BuyCurrency = c.Int(nullable: false),
                        UsdRate = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Ups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BatteryVoltage = c.Double(nullable: false),
                        BatteryVolume = c.Double(nullable: false),
                        BatteryCount = c.Int(nullable: false),
                        PowerCapacity = c.Double(nullable: false),
                        PhaseCount = c.Int(nullable: false),
                        InputRange = c.String(),
                        Name = c.String(),
                        Price = c.Double(nullable: false),
                        Weight = c.Double(nullable: false),
                        Rate = c.Double(nullable: false),
                        Photo = c.String(),
                        Description = c.String(),
                        WorkingConditions = c.String(),
                        Dimension = c.String(),
                        Warranty = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        IsAvailable = c.Boolean(nullable: false),
                        IsTopBuyed = c.Boolean(nullable: false),
                        IsNew = c.Boolean(nullable: false),
                        Discount = c.Double(nullable: false),
                        Info = c.String(),
                        SuppliedItems = c.String(),
                        Producer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Producers", t => t.Producer_Id)
                .Index(t => t.Producer_Id);
            
            CreateTable(
                "dbo.Converters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MaxChargingCurrent = c.Double(nullable: false),
                        PowerCapacity = c.Double(nullable: false),
                        PhaseCount = c.Int(nullable: false),
                        InputRange = c.String(),
                        Name = c.String(),
                        Price = c.Double(nullable: false),
                        Weight = c.Double(nullable: false),
                        Rate = c.Double(nullable: false),
                        Photo = c.String(),
                        Description = c.String(),
                        WorkingConditions = c.String(),
                        Dimension = c.String(),
                        Warranty = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        IsAvailable = c.Boolean(nullable: false),
                        IsTopBuyed = c.Boolean(nullable: false),
                        IsNew = c.Boolean(nullable: false),
                        Discount = c.Double(nullable: false),
                        Info = c.String(),
                        SuppliedItems = c.String(),
                        Producer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Producers", t => t.Producer_Id)
                .Index(t => t.Producer_Id);
            
            CreateTable(
                "dbo.Batteries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Voltage = c.Double(nullable: false),
                        Volume = c.Double(nullable: false),
                        Name = c.String(),
                        Price = c.Double(nullable: false),
                        Weight = c.Double(nullable: false),
                        Rate = c.Double(nullable: false),
                        Photo = c.String(),
                        Description = c.String(),
                        WorkingConditions = c.String(),
                        Dimension = c.String(),
                        Warranty = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        IsAvailable = c.Boolean(nullable: false),
                        IsTopBuyed = c.Boolean(nullable: false),
                        IsNew = c.Boolean(nullable: false),
                        Discount = c.Double(nullable: false),
                        Info = c.String(),
                        SuppliedItems = c.String(),
                        Producer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Producers", t => t.Producer_Id)
                .Index(t => t.Producer_Id);
            
            CreateTable(
                "dbo.Chargers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OutputVoltage = c.String(),
                        OutputCurrent = c.Double(nullable: false),
                        InputRange = c.String(),
                        Name = c.String(),
                        Price = c.Double(nullable: false),
                        Weight = c.Double(nullable: false),
                        Rate = c.Double(nullable: false),
                        Photo = c.String(),
                        Description = c.String(),
                        WorkingConditions = c.String(),
                        Dimension = c.String(),
                        Warranty = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        IsAvailable = c.Boolean(nullable: false),
                        IsTopBuyed = c.Boolean(nullable: false),
                        IsNew = c.Boolean(nullable: false),
                        Discount = c.Double(nullable: false),
                        Info = c.String(),
                        SuppliedItems = c.String(),
                        Producer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Producers", t => t.Producer_Id)
                .Index(t => t.Producer_Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        User = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        City = c.String(),
                        Address = c.String(),
                        CreationDate = c.DateTime(nullable: false),
                        Comment = c.String(),
                        Status = c.Int(nullable: false),
                        CloseDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrderItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Pid = c.String(),
                        Quantity = c.Int(nullable: false),
                        TotalItemPrice = c.Double(nullable: false),
                        UnitPrice = c.Double(nullable: false),
                        Order_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.Order_Id)
                .Index(t => t.Order_Id);
            
            CreateTable(
                "dbo.VoltageRelays",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullLoadCurrent = c.Double(nullable: false),
                        FullLoadPowerCapacity = c.Double(nullable: false),
                        NominalCurrent = c.Double(nullable: false),
                        DisplayVoltageRange = c.String(),
                        UpperLimitClearance = c.String(),
                        LowerLimitClearance = c.String(),
                        MinOperationValue = c.String(),
                        MaxOperationValue = c.String(),
                        MeasurementError = c.String(),
                        IndicationError = c.String(),
                        InputRange = c.String(),
                        Name = c.String(),
                        Price = c.Double(nullable: false),
                        Weight = c.Double(nullable: false),
                        Rate = c.Double(nullable: false),
                        Photo = c.String(),
                        Description = c.String(),
                        WorkingConditions = c.String(),
                        Dimension = c.String(),
                        Warranty = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        IsAvailable = c.Boolean(nullable: false),
                        IsTopBuyed = c.Boolean(nullable: false),
                        IsNew = c.Boolean(nullable: false),
                        Discount = c.Double(nullable: false),
                        Info = c.String(),
                        SuppliedItems = c.String(),
                        Producer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Producers", t => t.Producer_Id)
                .Index(t => t.Producer_Id);
            
            CreateTable(
                "dbo.CurrentRelays",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UpperLimitClearance = c.String(),
                        LowerLimitClearance = c.String(),
                        MinOperationValue = c.String(),
                        MaxOperationValue = c.String(),
                        MeasurementError = c.String(),
                        IndicationError = c.String(),
                        InputRange = c.String(),
                        Name = c.String(),
                        Price = c.Double(nullable: false),
                        Weight = c.Double(nullable: false),
                        Rate = c.Double(nullable: false),
                        Photo = c.String(),
                        Description = c.String(),
                        WorkingConditions = c.String(),
                        Dimension = c.String(),
                        Warranty = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        IsAvailable = c.Boolean(nullable: false),
                        IsTopBuyed = c.Boolean(nullable: false),
                        IsNew = c.Boolean(nullable: false),
                        Discount = c.Double(nullable: false),
                        Info = c.String(),
                        SuppliedItems = c.String(),
                        Producer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Producers", t => t.Producer_Id)
                .Index(t => t.Producer_Id);
            
            CreateTable(
                "dbo.TemperatureRelays",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MinOperationValue = c.String(),
                        MaxOperationValue = c.String(),
                        MeasurementError = c.String(),
                        IndicationError = c.String(),
                        InputRange = c.String(),
                        Name = c.String(),
                        Price = c.Double(nullable: false),
                        Weight = c.Double(nullable: false),
                        Rate = c.Double(nullable: false),
                        Photo = c.String(),
                        Description = c.String(),
                        WorkingConditions = c.String(),
                        Dimension = c.String(),
                        Warranty = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        IsAvailable = c.Boolean(nullable: false),
                        IsTopBuyed = c.Boolean(nullable: false),
                        IsNew = c.Boolean(nullable: false),
                        Discount = c.Double(nullable: false),
                        Info = c.String(),
                        SuppliedItems = c.String(),
                        Producer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Producers", t => t.Producer_Id)
                .Index(t => t.Producer_Id);
            
            CreateTable(
                "dbo.TimeRelays",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MinOperationValue = c.String(),
                        MaxOperationValue = c.String(),
                        MeasurementError = c.String(),
                        IndicationError = c.String(),
                        InputRange = c.String(),
                        Name = c.String(),
                        Price = c.Double(nullable: false),
                        Weight = c.Double(nullable: false),
                        Rate = c.Double(nullable: false),
                        Photo = c.String(),
                        Description = c.String(),
                        WorkingConditions = c.String(),
                        Dimension = c.String(),
                        Warranty = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        IsAvailable = c.Boolean(nullable: false),
                        IsTopBuyed = c.Boolean(nullable: false),
                        IsNew = c.Boolean(nullable: false),
                        Discount = c.Double(nullable: false),
                        Info = c.String(),
                        SuppliedItems = c.String(),
                        Producer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Producers", t => t.Producer_Id)
                .Index(t => t.Producer_Id);
            
            CreateTable(
                "dbo.Voltmeters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MinOperationValue = c.String(),
                        MaxOperationValue = c.String(),
                        MeasurementError = c.String(),
                        IndicationError = c.String(),
                        InputRange = c.String(),
                        Name = c.String(),
                        Price = c.Double(nullable: false),
                        Weight = c.Double(nullable: false),
                        Rate = c.Double(nullable: false),
                        Photo = c.String(),
                        Description = c.String(),
                        WorkingConditions = c.String(),
                        Dimension = c.String(),
                        Warranty = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        IsAvailable = c.Boolean(nullable: false),
                        IsTopBuyed = c.Boolean(nullable: false),
                        IsNew = c.Boolean(nullable: false),
                        Discount = c.Double(nullable: false),
                        Info = c.String(),
                        SuppliedItems = c.String(),
                        Producer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Producers", t => t.Producer_Id)
                .Index(t => t.Producer_Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Voltmeters", new[] { "Producer_Id" });
            DropIndex("dbo.TimeRelays", new[] { "Producer_Id" });
            DropIndex("dbo.TemperatureRelays", new[] { "Producer_Id" });
            DropIndex("dbo.CurrentRelays", new[] { "Producer_Id" });
            DropIndex("dbo.VoltageRelays", new[] { "Producer_Id" });
            DropIndex("dbo.OrderItems", new[] { "Order_Id" });
            DropIndex("dbo.Chargers", new[] { "Producer_Id" });
            DropIndex("dbo.Batteries", new[] { "Producer_Id" });
            DropIndex("dbo.Converters", new[] { "Producer_Id" });
            DropIndex("dbo.Ups", new[] { "Producer_Id" });
            DropIndex("dbo.Avrs", new[] { "Producer_Id" });
            DropForeignKey("dbo.Voltmeters", "Producer_Id", "dbo.Producers");
            DropForeignKey("dbo.TimeRelays", "Producer_Id", "dbo.Producers");
            DropForeignKey("dbo.TemperatureRelays", "Producer_Id", "dbo.Producers");
            DropForeignKey("dbo.CurrentRelays", "Producer_Id", "dbo.Producers");
            DropForeignKey("dbo.VoltageRelays", "Producer_Id", "dbo.Producers");
            DropForeignKey("dbo.OrderItems", "Order_Id", "dbo.Orders");
            DropForeignKey("dbo.Chargers", "Producer_Id", "dbo.Producers");
            DropForeignKey("dbo.Batteries", "Producer_Id", "dbo.Producers");
            DropForeignKey("dbo.Converters", "Producer_Id", "dbo.Producers");
            DropForeignKey("dbo.Ups", "Producer_Id", "dbo.Producers");
            DropForeignKey("dbo.Avrs", "Producer_Id", "dbo.Producers");
            DropTable("dbo.Voltmeters");
            DropTable("dbo.TimeRelays");
            DropTable("dbo.TemperatureRelays");
            DropTable("dbo.CurrentRelays");
            DropTable("dbo.VoltageRelays");
            DropTable("dbo.OrderItems");
            DropTable("dbo.Orders");
            DropTable("dbo.Chargers");
            DropTable("dbo.Batteries");
            DropTable("dbo.Converters");
            DropTable("dbo.Ups");
            DropTable("dbo.Producers");
            DropTable("dbo.Avrs");
        }
    }
}
