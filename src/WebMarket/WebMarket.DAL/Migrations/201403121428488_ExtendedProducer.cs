namespace WebMarket.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExtendedProducer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Producers", "HomePage", c => c.String());
            AddColumn("dbo.Producers", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Producers", "Description");
            DropColumn("dbo.Producers", "HomePage");
        }
    }
}
