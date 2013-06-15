namespace WebMarket.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Callback : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Callbacks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Phone = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Callbacks");
        }
    }
}
