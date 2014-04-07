using System.Data.Entity.Migrations;

namespace WebMarket.Repository.Migrations
{
  public sealed class Configuration : DbMigrationsConfiguration<WebMarketDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(WebMarketDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<Product>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
