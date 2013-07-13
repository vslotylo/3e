namespace WebMarket.DAL.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class RemovedTotalItemPrice : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.OrderItems", "TotalItemPrice");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderItems", "TotalItemPrice", c => c.Double(nullable: false));
        }
    }
}
