namespace WebMarket.DAL.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class DeletedEntitiesTables : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Avrs", "Producer_Id", "dbo.Producers");
            DropForeignKey("dbo.Ups", "Producer_Id", "dbo.Producers");
            DropForeignKey("dbo.Converters", "Producer_Id", "dbo.Producers");
            DropForeignKey("dbo.Batteries", "Producer_Id", "dbo.Producers");
            DropForeignKey("dbo.Chargers", "Producer_Id", "dbo.Producers");
            DropForeignKey("dbo.VoltageRelays", "Producer_Id", "dbo.Producers");
            DropForeignKey("dbo.CurrentRelays", "Producer_Id", "dbo.Producers");
            DropForeignKey("dbo.TemperatureRelays", "Producer_Id", "dbo.Producers");
            DropForeignKey("dbo.TimeRelays", "Producer_Id", "dbo.Producers");
            DropForeignKey("dbo.Voltmeters", "Producer_Id", "dbo.Producers");
            DropIndex("dbo.Avrs", new[] { "Producer_Id" });
            DropIndex("dbo.Ups", new[] { "Producer_Id" });
            DropIndex("dbo.Converters", new[] { "Producer_Id" });
            DropIndex("dbo.Batteries", new[] { "Producer_Id" });
            DropIndex("dbo.Chargers", new[] { "Producer_Id" });
            DropIndex("dbo.VoltageRelays", new[] { "Producer_Id" });
            DropIndex("dbo.CurrentRelays", new[] { "Producer_Id" });
            DropIndex("dbo.TemperatureRelays", new[] { "Producer_Id" });
            DropIndex("dbo.TimeRelays", new[] { "Producer_Id" });
            DropIndex("dbo.Voltmeters", new[] { "Producer_Id" });
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
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
                        Availability = c.Int(nullable: false),
                        DisplayClass = c.Int(nullable: false),
                        Discount = c.Double(nullable: false),
                        Info = c.String(),
                        SuppliedItems = c.String(),
                        ProductCategory_Id = c.Int(),
                        Producer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductCategories", t => t.ProductCategory_Id)
                .ForeignKey("dbo.Producers", t => t.Producer_Id)
                .Index(t => t.ProductCategory_Id)
                .Index(t => t.Producer_Id);
            
            CreateTable(
                "dbo.ProductCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.Avrs");
            DropTable("dbo.Ups");
            DropTable("dbo.Converters");
            DropTable("dbo.Batteries");
            DropTable("dbo.Chargers");
            DropTable("dbo.VoltageRelays");
            DropTable("dbo.CurrentRelays");
            DropTable("dbo.TemperatureRelays");
            DropTable("dbo.TimeRelays");
            DropTable("dbo.Voltmeters");
        }
        
        public override void Down()
        {
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
                        Availability = c.Int(nullable: false),
                        DisplayClass = c.Int(nullable: false),
                        Discount = c.Double(nullable: false),
                        Info = c.String(),
                        SuppliedItems = c.String(),
                        Producer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
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
                        Availability = c.Int(nullable: false),
                        DisplayClass = c.Int(nullable: false),
                        Discount = c.Double(nullable: false),
                        Info = c.String(),
                        SuppliedItems = c.String(),
                        Producer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
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
                        Availability = c.Int(nullable: false),
                        DisplayClass = c.Int(nullable: false),
                        Discount = c.Double(nullable: false),
                        Info = c.String(),
                        SuppliedItems = c.String(),
                        Producer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
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
                        Availability = c.Int(nullable: false),
                        DisplayClass = c.Int(nullable: false),
                        Discount = c.Double(nullable: false),
                        Info = c.String(),
                        SuppliedItems = c.String(),
                        Producer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
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
                        Availability = c.Int(nullable: false),
                        DisplayClass = c.Int(nullable: false),
                        Discount = c.Double(nullable: false),
                        Info = c.String(),
                        SuppliedItems = c.String(),
                        Producer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
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
                        Availability = c.Int(nullable: false),
                        DisplayClass = c.Int(nullable: false),
                        Discount = c.Double(nullable: false),
                        Info = c.String(),
                        SuppliedItems = c.String(),
                        Producer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
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
                        Availability = c.Int(nullable: false),
                        DisplayClass = c.Int(nullable: false),
                        Discount = c.Double(nullable: false),
                        Info = c.String(),
                        SuppliedItems = c.String(),
                        Producer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
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
                        Availability = c.Int(nullable: false),
                        DisplayClass = c.Int(nullable: false),
                        Discount = c.Double(nullable: false),
                        Info = c.String(),
                        SuppliedItems = c.String(),
                        Producer_Id = c.Int(),
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
                        Availability = c.Int(nullable: false),
                        DisplayClass = c.Int(nullable: false),
                        Discount = c.Double(nullable: false),
                        Info = c.String(),
                        SuppliedItems = c.String(),
                        Producer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
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
                        Availability = c.Int(nullable: false),
                        DisplayClass = c.Int(nullable: false),
                        Discount = c.Double(nullable: false),
                        Info = c.String(),
                        SuppliedItems = c.String(),
                        Producer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropIndex("dbo.Products", new[] { "Producer_Id" });
            DropIndex("dbo.Products", new[] { "ProductCategory_Id" });
            DropForeignKey("dbo.Products", "Producer_Id", "dbo.Producers");
            DropForeignKey("dbo.Products", "ProductCategory_Id", "dbo.ProductCategories");
            DropTable("dbo.ProductCategories");
            DropTable("dbo.Products");
            CreateIndex("dbo.Voltmeters", "Producer_Id");
            CreateIndex("dbo.TimeRelays", "Producer_Id");
            CreateIndex("dbo.TemperatureRelays", "Producer_Id");
            CreateIndex("dbo.CurrentRelays", "Producer_Id");
            CreateIndex("dbo.VoltageRelays", "Producer_Id");
            CreateIndex("dbo.Chargers", "Producer_Id");
            CreateIndex("dbo.Batteries", "Producer_Id");
            CreateIndex("dbo.Converters", "Producer_Id");
            CreateIndex("dbo.Ups", "Producer_Id");
            CreateIndex("dbo.Avrs", "Producer_Id");
            AddForeignKey("dbo.Voltmeters", "Producer_Id", "dbo.Producers", "Id");
            AddForeignKey("dbo.TimeRelays", "Producer_Id", "dbo.Producers", "Id");
            AddForeignKey("dbo.TemperatureRelays", "Producer_Id", "dbo.Producers", "Id");
            AddForeignKey("dbo.CurrentRelays", "Producer_Id", "dbo.Producers", "Id");
            AddForeignKey("dbo.VoltageRelays", "Producer_Id", "dbo.Producers", "Id");
            AddForeignKey("dbo.Chargers", "Producer_Id", "dbo.Producers", "Id");
            AddForeignKey("dbo.Batteries", "Producer_Id", "dbo.Producers", "Id");
            AddForeignKey("dbo.Converters", "Producer_Id", "dbo.Producers", "Id");
            AddForeignKey("dbo.Ups", "Producer_Id", "dbo.Producers", "Id");
            AddForeignKey("dbo.Avrs", "Producer_Id", "dbo.Producers", "Id");
        }
    }
}
