namespace WebMarket.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CategoryAddedTitleDetailsPossessive : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "TitleDetailsPossessive", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categories", "TitleDetailsPossessive");
        }
    }
}
