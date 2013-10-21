namespace WebMarket.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedGroups : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.Products", "SubCategory_Id", "dbo.SubCategories");
            DropIndex("dbo.Products", new[] { "Category_Id" });
            DropIndex("dbo.Products", new[] { "SubCategory_Id" });
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 128),
                        DisplayName = c.String(),
                    })
                .PrimaryKey(t => t.Name);
            
            AddColumn("dbo.Products", "CategoryName2", c => c.String());
            AddColumn("dbo.Products", "GroupName2", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Products", "Category_ID", c => c.Int());
            AlterColumn("dbo.Categories", "ID", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.Categories", new[] { "Id" });
            AddPrimaryKey("dbo.Categories", "ID");
            AddForeignKey("dbo.Products", "Category_ID", "dbo.Categories", "ID");
            AddForeignKey("dbo.Products", "GroupName2", "dbo.Groups", "Name", cascadeDelete: true);
            CreateIndex("dbo.Products", "Category_ID");
            CreateIndex("dbo.Products", "GroupName2");
            DropColumn("dbo.Products", "CategoryName");
            DropColumn("dbo.Products", "SubCategoryName");
            DropColumn("dbo.Products", "SubCategory_Id");
            DropTable("dbo.SubCategories");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SubCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DisplayName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Products", "SubCategory_Id", c => c.Int());
            AddColumn("dbo.Products", "SubCategoryName", c => c.String());
            AddColumn("dbo.Products", "CategoryName", c => c.String());
            DropIndex("dbo.Products", new[] { "GroupName2" });
            DropIndex("dbo.Products", new[] { "Category_ID" });
            DropForeignKey("dbo.Products", "GroupName2", "dbo.Groups");
            DropForeignKey("dbo.Products", "Category_ID", "dbo.Categories");
            DropPrimaryKey("dbo.Categories", new[] { "ID" });
            AddPrimaryKey("dbo.Categories", "Id");
            AlterColumn("dbo.Categories", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Products", "Category_Id", c => c.Int());
            DropColumn("dbo.Products", "GroupName2");
            DropColumn("dbo.Products", "CategoryName2");
            DropTable("dbo.Groups");
            CreateIndex("dbo.Products", "SubCategory_Id");
            CreateIndex("dbo.Products", "Category_Id");
            AddForeignKey("dbo.Products", "SubCategory_Id", "dbo.SubCategories", "Id");
            AddForeignKey("dbo.Products", "Category_Id", "dbo.Categories", "Id");
        }
    }
}
