using System.Configuration;

namespace WebMarket.Repository.Core
{
    public class DbContextFactory : IDbContextFactory<WebMarketDbContext>
    {
        public WebMarketDbContext GetDataContext()
        {
            return new WebMarketDbContext(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        }
    }
}