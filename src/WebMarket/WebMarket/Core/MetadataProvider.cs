using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Hosting;
using OfficeOpenXml;
using WebMarket.DAL.Common;
using log4net;

namespace WebMarket.Core
{
    public class MetadataProvider
    {
        private static readonly MetadataProvider Instance;
        private readonly Dictionary<string, Metadata> dict;
        static MetadataProvider()
        {
            Instance = new MetadataProvider();
        }

        private MetadataProvider()
        {
            try
            {
                dict = new Dictionary<string, Metadata>();
                var context = new WebMarketDbContext();
                var categories = context.Categories.ToList();
                using (var fs = File.OpenRead(HostingEnvironment.MapPath(("~/App_Data/metadata/metadata.xlsx"))))
                {
                    var excelPackage = new ExcelPackage(fs);
                    var excelWorksheet = excelPackage.Workbook.Worksheets.FirstOrDefault(obj => !obj.Name.StartsWith("_"));
                    int row = 2;
                    int col = 1;
                    while (excelWorksheet.Cells[row, col].Value != null)
                    {
                        var categoryName = excelWorksheet.Cells[row, col].Value.ToString().ToLower();
                        col++;
                        var listTitle = excelWorksheet.Cells[row, col].Value.ToString();
                        col++;
                        var detailsTitle = excelWorksheet.Cells[row, col].Value.ToString();
                        col++;
                        var metadataList = excelWorksheet.Cells[row, col].Value.ToString();
                        col++;
                        var metadataDetails = excelWorksheet.Cells[row, col].Value.ToString();
                        var categoryDisplayName = categories.SingleOrDefault(obj => obj.Name == categoryName).DisplayName;
                        this.dict[categoryName] = new Metadata(categoryDisplayName, listTitle, detailsTitle, metadataList, metadataDetails);
                        row++;
                        col = 1;
                    }
                }
            }
            catch (Exception e)
            {
                var log = LogManager.GetLogger(typeof (MetadataProvider));
                log.Error(e);
                throw;
            }
            
        }

        public static MetadataProvider Current
        {
            get
            {
                return Instance;
            }
        }

        public Metadata GetMetadataInfo(string category)
        {
            return dict[category.ToLower()];
        }
    }
}