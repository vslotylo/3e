namespace WebMarket.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSubCategories : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "ProductCategory_Id", "dbo.ProductCategories");
            DropIndex("dbo.Products", new[] { "ProductCategory_Id" });
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SubCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Products", "CategoryName", c => c.String());
            AddColumn("dbo.Products", "SubCategoryName", c => c.String());
            AddColumn("dbo.Products", "Category_Id", c => c.Int());
            AddColumn("dbo.Products", "SubCategory_Id", c => c.Int());
            AddForeignKey("dbo.Products", "Category_Id", "dbo.Categories", "Id");
            AddForeignKey("dbo.Products", "SubCategory_Id", "dbo.SubCategories", "Id");
            CreateIndex("dbo.Products", "Category_Id");
            CreateIndex("dbo.Products", "SubCategory_Id");
            DropColumn("dbo.Products", "ProductCategory_Id");
            DropTable("dbo.ProductCategories");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ProductCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Products", "ProductCategory_Id", c => c.Int());
            DropIndex("dbo.Products", new[] { "SubCategory_Id" });
            DropIndex("dbo.Products", new[] { "Category_Id" });
            DropForeignKey("dbo.Products", "SubCategory_Id", "dbo.SubCategories");
            DropForeignKey("dbo.Products", "Category_Id", "dbo.Categories");
            DropColumn("dbo.Products", "SubCategory_Id");
            DropColumn("dbo.Products", "Category_Id");
            DropColumn("dbo.Products", "SubCategoryName");
            DropColumn("dbo.Products", "CategoryName");
            DropTable("dbo.SubCategories");
            DropTable("dbo.Categories");
            CreateIndex("dbo.Products", "ProductCategory_Id");
            AddForeignKey("dbo.Products", "ProductCategory_Id", "dbo.ProductCategories", "Id");
        }
    }
}
