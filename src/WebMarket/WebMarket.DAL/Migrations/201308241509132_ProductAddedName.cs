namespace WebMarket.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductAddedName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "DisplayName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "DisplayName");
        }
    }
}
