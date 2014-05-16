using WebMarket.Repository.Extensions;

namespace WebMarket.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLastModifiedDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "LastModifiedDate", c => c.DateTime(nullable: false, defaultValue: DateTime.Now.ToUkrainianTimeZone()));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "LastModifiedDate");
        }
    }
}
