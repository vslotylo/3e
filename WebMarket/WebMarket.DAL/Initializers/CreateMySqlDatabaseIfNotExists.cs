using System;
using System.Data.Entity;

namespace WebMarket.DAL.Initializers
{
    public class CreateMySqlDatabaseIfNotExists<TContext> : MySqlDatabaseInitializer<TContext> where TContext : DbContext
    {
        public override void InitializeDatabase(TContext context)
        {
            if (context.Database.Exists())
            {
                if (!context.Database.CompatibleWithModel(false))
                {
                    throw new InvalidOperationException("The model has changed!");
                }
            }
            else
            {
                this.CreateMySqlDatabase(context);
            }
        }
    }
}
