namespace WebMarket.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedOrderEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderItems", "ProductId", c => c.Int(nullable: false));
            DropColumn("dbo.OrderItems", "Pid");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderItems", "Pid", c => c.String());
            DropColumn("dbo.OrderItems", "ProductId");
        }
    }
}
