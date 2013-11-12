using System.Collections.Generic;
using System.IO;
using System.Linq;
using OfficeOpenXml;

namespace WebMarket.Tools.Products
{
    class ProductImageNameProcessor : XlsxProcessor
    {
        private readonly string tilesPath;
        private readonly string categoryName;
        private readonly string sheetName;
        private readonly string sourceDirectory;
        private readonly string inputDirectory;
        private readonly string outputDirectory;

        public ProductImageNameProcessor(string path)
            : base(path)
        {
            tilesPath = @"D:\Projects\3e\src\WebMarket\WebMarket\Content\tiles";
            categoryName = "currentrelay";
            sheetName = "current-relay";
            if (string.IsNullOrEmpty(sheetName))
            {
                sheetName = categoryName;
            }

            sourceDirectory = Path.Combine(tilesPath, categoryName);
            inputDirectory = Path.Combine(tilesPath, string.Format("{0}_input", categoryName));
            outputDirectory = Path.Combine(tilesPath, string.Format("{0}_output", categoryName));
        }

        protected override void BeginProcessing()
        {
            try
            {
                Directory.Delete(inputDirectory, true);
                Directory.Delete(outputDirectory, true);
            }
            catch
            {

            }

            Directory.CreateDirectory(inputDirectory);
            Directory.CreateDirectory(outputDirectory);
            Directory.GetFiles(sourceDirectory).ToList().ForEach(f => File.Copy(f, inputDirectory + "\\" + f.Substring(f.LastIndexOf("\\"), f.Length - f.LastIndexOf("\\")), true));

        }

        protected override void Processing(ExcelPackage excelPackage)
        {
            var worksheet = excelPackage.Workbook.Worksheets[sheetName];

            int row = 3;
            var products = new List<Product>();
            var tiles = Directory.GetFiles(inputDirectory);
            while (worksheet.Cells[row, 2].Value != null)
            {
                var product = new Product
                {
                    Name = worksheet.Cells[row, 2].Value.ToString(),
                    Photo = worksheet.Cells[row, 6].Value.ToString(),
                    Producer = worksheet.Cells[row, 7].Value.ToString(),
                    Index = row
                };
                if (string.IsNullOrEmpty(product.Name))
                {
                    break;
                }

                products.Add(product);
                row++;
            }


            foreach (var tile in tiles)
            {
                var product = products.FirstOrDefault(obj => tile.Contains(obj.Photo));
                if (product == null)
                {
                    continue;
                }

                var photoName = product.Name;
                if (!product.Photo.Contains("."))
                {
                    photoName += ".jpg";
                }
                else
                {
                    var extension = product.Photo.Substring(product.Photo.IndexOf("."), product.Photo.Length - product.Photo.IndexOf("."));
                    photoName += extension;
                }

                string newPhotoName = Path.Combine(outputDirectory, string.Format("{0}-{1}", product.Producer, photoName));
                File.Move(tile, newPhotoName);

                var pos = newPhotoName.LastIndexOf("\\") + 1;
                newPhotoName = newPhotoName.Substring(pos, newPhotoName.Length - pos);
                if (newPhotoName.EndsWith(".jpg"))
                {
                    newPhotoName = newPhotoName.Substring(0, newPhotoName.Length - 4);
                }

                worksheet.Cells[product.Index, 8].Value = newPhotoName;
                products.Remove(product);
            }
        }
    }
}
