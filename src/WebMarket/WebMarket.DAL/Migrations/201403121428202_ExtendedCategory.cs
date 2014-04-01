namespace WebMarket.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExtendedCategory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "TitleList", c => c.String());
            AddColumn("dbo.Categories", "TitleDetails", c => c.String());
            AddColumn("dbo.Categories", "MetaListDescription", c => c.String());
            AddColumn("dbo.Categories", "MetadataDetailsDescription", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categories", "MetadataDetailsDescription");
            DropColumn("dbo.Categories", "MetaListDescription");
            DropColumn("dbo.Categories", "TitleDetails");
            DropColumn("dbo.Categories", "TitleList");
        }
    }
}
