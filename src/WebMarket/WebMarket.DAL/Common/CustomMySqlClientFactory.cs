using System.Data.Common;
using System.Data.Entity.Infrastructure;
using MySql.Data.MySqlClient;

namespace WebMarket.DAL.Common
{
    public sealed class CustomMySqlClientFactory : IDbConnectionFactory
    {
        //todo verify single connection is used
        public DbConnection CreateConnection(string nameOrConnectionString)
        {
            return new MySqlConnection(nameOrConnectionString);
        }
    }
}
