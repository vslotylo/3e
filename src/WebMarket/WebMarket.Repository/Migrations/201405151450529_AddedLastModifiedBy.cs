namespace WebMarket.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedLastModifiedBy : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "LastModifiedBy", c => c.String(nullable: false, defaultValue: "system"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "LastModifiedBy");
        }
    }
}
