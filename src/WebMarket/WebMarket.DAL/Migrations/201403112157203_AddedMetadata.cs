namespace WebMarket.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedMetadata : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Metadatas",
                c => new
                    {
                        CategoryName = c.String(nullable: false, maxLength: 128),
                        TitleList = c.String(),
                        TitleDetails = c.String(),
                        MetaListDescription = c.String(),
                        MetadataDetailsDescription = c.String(),
                    })
                .PrimaryKey(t => t.CategoryName);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Metadatas");
        }
    }
}
