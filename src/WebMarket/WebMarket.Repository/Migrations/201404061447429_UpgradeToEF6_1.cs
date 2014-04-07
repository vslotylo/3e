using System.Data.Entity.Migrations;

namespace WebMarket.Repository.Migrations
{
    public partial class UpgradeToEf61 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Callbacks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Phone = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        Url = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 128),
                        DisplayName = c.String(),
                        TitleList = c.String(),
                        TitleDetails = c.String(),
                        MetaListDescription = c.String(),
                        MetadataDetailsDescription = c.String(),
                    })
                .PrimaryKey(t => t.Name);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 128),
                        DisplayName = c.String(),
                    })
                .PrimaryKey(t => t.Name);
            
            CreateTable(
                "dbo.OrderItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        UnitPrice = c.Double(nullable: false),
                        Order_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.Order_Id)
                .Index(t => t.Order_Id);
            
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
                "dbo.Producers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DisplayName = c.String(),
                        HomePage = c.String(),
                        Description = c.String(),
                        BuyCurrency = c.Int(nullable: false),
                        UsdRate = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DisplayName = c.String(),
                        Name = c.String(),
                        Price = c.Double(nullable: false),
                        Weight = c.Double(nullable: false),
                        Rate = c.Double(nullable: false),
                        Photo = c.String(),
                        Description = c.String(),
                        WorkingConditions = c.String(),
                        SuppliedItems = c.String(),
                        Dimension = c.String(),
                        Warranty = c.Int(nullable: false),
                        CategoryName = c.String(nullable: false, maxLength: 128),
                        GroupName = c.String(nullable: false, maxLength: 128),
                        Discount = c.Double(nullable: false),
                        Info = c.String(),
                        Availability = c.Int(nullable: false),
                        DisplayClass = c.Int(nullable: false),
                        Producer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryName, cascadeDelete: true)
                .ForeignKey("dbo.Groups", t => t.GroupName, cascadeDelete: true)
                .ForeignKey("dbo.Producers", t => t.Producer_Id)
                .Index(t => t.CategoryName)
                .Index(t => t.GroupName)
                .Index(t => t.Producer_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "Producer_Id", "dbo.Producers");
            DropForeignKey("dbo.Products", "GroupName", "dbo.Groups");
            DropForeignKey("dbo.Products", "CategoryName", "dbo.Categories");
            DropForeignKey("dbo.OrderItems", "Order_Id", "dbo.Orders");
            DropIndex("dbo.Products", new[] { "Producer_Id" });
            DropIndex("dbo.Products", new[] { "GroupName" });
            DropIndex("dbo.Products", new[] { "CategoryName" });
            DropIndex("dbo.OrderItems", new[] { "Order_Id" });
            DropTable("dbo.Products");
            DropTable("dbo.Producers");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderItems");
            DropTable("dbo.Groups");
            DropTable("dbo.Categories");
            DropTable("dbo.Callbacks");
        }
    }
}
