namespace WebMarket.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDisplayName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "DisplayName", c => c.String());
            AddColumn("dbo.SubCategories", "DisplayName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SubCategories", "DisplayName");
            DropColumn("dbo.Categories", "DisplayName");
        }
    }
}
