using System;
using OfficeOpenXml;

namespace WebMarket.Tools.Products
{
    public class ProductNameProcessor : XlsxProcessor
    {
        public ProductNameProcessor(string path) : base(path)
        {
        }

        protected override void Processing(ExcelPackage excelPackage)
        {
            foreach (var sheet in excelPackage.Workbook.Worksheets)
            {
                var nameIndex = GetColumnIndexByName(sheet, "Name");
                var producerIndex = GetColumnIndexByName(sheet, "Producer");

                int row = 3;
                while (sheet.Cells[row, nameIndex].Value != null)
                {
                    var productName = sheet.Cells[row, nameIndex].Value.ToString();
                    var producerName = sheet.Cells[row, producerIndex].Value.ToString();
                    if (string.IsNullOrEmpty(productName) || string.IsNullOrEmpty(producerName))
                    {
                        throw new Exception(string.Format("Product name or producer name is empty: Sheet name: {0}, Row: {1}", sheet.Name, row));
                    }

                    var newName = string.Format("{0}-{1}", producerName.Trim(), productName.Trim());
                    sheet.Cells[row, nameIndex].Value = newName;
                    row++;
                }
            }
        }
    }
}
