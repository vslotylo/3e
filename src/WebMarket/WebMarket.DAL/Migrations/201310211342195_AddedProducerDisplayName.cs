namespace WebMarket.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedProducerDisplayName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Producers", "DisplayName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Producers", "DisplayName");
        }
    }
}
