using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Xml;
using MySql.Data.MySqlClient;

namespace WebMarket.DAL.Initializers
{
    public abstract class MySqlDatabaseInitializer<TContext> : IDatabaseInitializer<TContext> where TContext : DbContext
    {
        public abstract void InitializeDatabase(TContext context);

        protected void CreateMySqlDatabase(TContext context)
        {
            try
            {
                // Create as much of the database as we can
                context.Database.Create();
                this.Seed(context);
                // No exception? Don't need a workaround
                return;
            }
            catch (MySqlException ex)
            {
                // Ignore the parse exception
                if (ex.Number != 1064)
                {
                    throw;
                }
            }

            // Manually create the metadata table
            using (var connection = ((MySqlConnection)context
                .Database.Connection).Clone())
            using (var command = connection.CreateCommand())
            {
                command.CommandText =
    @"
CREATE TABLE __MigrationHistory (
    MigrationId mediumtext NOT NULL,
    Model mediumblob NOT NULL,
    ProductVersion mediumtext NOT NULL);

ALTER TABLE __MigrationHistory
ADD PRIMARY KEY (MigrationId(255));

INSERT INTO __MigrationHistory (
    MigrationId,
    Model,
    ProductVersion)
VALUES (
    'InitialCreate',
    @Model,
    @ProductVersion);
";
                command.Parameters.AddWithValue(
                    "@Model",
                    this.GetModel(context));
                command.Parameters.AddWithValue(
                    "@ProductVersion",
                    this.GetProductVersion());

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        private byte[] GetModel(TContext context)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var gzipStream = new GZipStream(
                    memoryStream,
                    CompressionMode.Compress))
                using (var xmlWriter = XmlWriter.Create(
                    gzipStream,
                    new XmlWriterSettings { Indent = true }))
                {
                    EdmxWriter.WriteEdmx(context, xmlWriter);
                }

                return memoryStream.ToArray();
            }
        }

        private string GetProductVersion()
        {
            return typeof(DbContext).Assembly
                .GetCustomAttributes(false)
                .OfType<AssemblyInformationalVersionAttribute>()
                .Single()
                .InformationalVersion;
        }

        protected virtual void Seed(TContext context)
        {
            
        }
    }
}
