namespace WebMarket.DAL.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddedStatusOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Callbacks", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Callbacks", "Status");
        }
    }
}
