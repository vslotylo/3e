using System.Data.Entity;

namespace WebMarket.DAL.Initializers
{
    public class DropCreateMySqlDatabaseIfModelChanges<TContext>
     : MySqlDatabaseInitializer<TContext> where TContext : DbContext
    {
        public override void InitializeDatabase(TContext context)
        {
            bool needsNewDb = false;
            if (context.Database.Exists())
            {
                if (!context.Database.CompatibleWithModel(false))
                {
                    context.Database.Delete();
                    needsNewDb = true;
                }
            }
            else
            {
                needsNewDb = true;
            }
            if (needsNewDb) this.CreateMySqlDatabase(context);
        }
    }
}
