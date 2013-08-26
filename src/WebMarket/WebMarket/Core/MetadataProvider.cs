using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Hosting;
using OfficeOpenXml;
using WebMarket.DAL.Common;

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
            dict = new Dictionary<string, Metadata>();
            var context = new WebMarketDbContext();
            var categories = context.Categories.ToList();
            using (var fs = File.OpenRead(HostingEnvironment.MapPath(("~/Content/metadata/metadata.xlsx"))))
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
                    var detailTitle = excelWorksheet.Cells[row, col].Value.ToString();
                    var categoryDisplayName = categories.SingleOrDefault(obj => obj.Name == categoryName).DisplayName;
                    this.dict[categoryName] = new Metadata(categoryDisplayName, listTitle, detailTitle);
                    row++;
                    col = 1;
                }
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