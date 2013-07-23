using System.Data.Entity;

namespace WebMarket.DAL.Initializers
{
    public class DropCreateMySqlDatabaseAlways<TContext>
      : MySqlDatabaseInitializer<TContext> where TContext : DbContext
    {
        public override void InitializeDatabase(TContext context)
        {
            context.Database.Delete();
            this.CreateMySqlDatabase(context);
        }
    }
}