namespace WebMarket.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedType : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Products", "Type");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Type", c => c.Int(nullable: false));
        }
    }
}
