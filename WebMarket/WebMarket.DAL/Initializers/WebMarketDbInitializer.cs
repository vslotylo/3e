using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WebMarket.DAL.Common;
using WebMarket.DAL.Data.Import;
using WebMarket.DAL.Exceptions;

namespace WebMarket.DAL.Initializers
{
    public class WebMarketDbInitializer : CreateMySqlDatabaseIfNotExists<WebMarketDbContext>
    {
        private const string FormatXlsx = "xlsx";

        private readonly string dataDirPath;

        public WebMarketDbInitializer(string dataDirPath)
        {
            this.dataDirPath = dataDirPath;
        }

        protected override void Seed(WebMarketDbContext context)
        {
            var importer = new DataImporter();
            var dInfo = new DirectoryInfo(this.dataDirPath);
            var files = dInfo.GetFiles(string.Format("*.{0}", FormatXlsx));
            var producersFile = files.FirstOrDefault(obj => string.Compare(obj.Name, string.Format("producer.{0}", FormatXlsx), StringComparison.OrdinalIgnoreCase) == 0);
            if (producersFile == null)
            {
                throw new EntityImportException("Counld not find producer data file.");
            }
            using (var fs = producersFile.OpenRead())
            {
                importer.Import(fs, context);
            }

            context.SaveChanges();
            
            foreach (var dataFile in files.Except(new [] { producersFile }))
            {
                using (var fs = dataFile.OpenRead())
                {
                    importer.Import(fs, context);
                }
            }
            
            context.SaveChanges();
        }
    }
}
