namespace WebMarket.Repository.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddedDescriptionRU : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "DescriptionRU", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "DescriptionRU");
        }
    }
}
