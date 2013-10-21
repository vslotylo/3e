namespace WebMarket.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedCategoryKey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "Category_ID", "dbo.Categories");
            DropIndex("dbo.Products", new[] { "Category_ID" });
            RenameColumn(table: "dbo.Products", name: "Category_ID", newName: "CategoryName");
            RenameColumn(table: "dbo.Products", name: "GroupName2", newName: "GroupName");
            AlterColumn("dbo.Products", "CategoryName", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Categories", "Name", c => c.String(nullable: false, maxLength: 128));
            DropPrimaryKey("dbo.Categories", new[] { "ID" });
            AddPrimaryKey("dbo.Categories", "Name");
            AddForeignKey("dbo.Products", "CategoryName", "dbo.Categories", "Name", cascadeDelete: true);
            CreateIndex("dbo.Products", "CategoryName");
            DropColumn("dbo.Products", "CategoryName2");
            DropColumn("dbo.Categories", "ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Categories", "ID", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Products", "CategoryName2", c => c.String());
            DropIndex("dbo.Products", new[] { "CategoryName" });
            DropForeignKey("dbo.Products", "CategoryName", "dbo.Categories");
            DropPrimaryKey("dbo.Categories", new[] { "Name" });
            AddPrimaryKey("dbo.Categories", "ID");
            AlterColumn("dbo.Categories", "Name", c => c.String());
            RenameColumn(table: "dbo.Products", name: "GroupName", newName: "GroupName2");
            RenameColumn(table: "dbo.Products", name: "CategoryName", newName: "Category_ID");
            CreateIndex("dbo.Products", "Category_ID");
            AddForeignKey("dbo.Products", "Category_ID", "dbo.Categories", "ID");
        }
    }
}
