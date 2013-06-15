using System.Data.Entity.Migrations;
namespace WebMarket.DAL.Migrations
{
    public partial class Callback : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Callbacks",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
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
