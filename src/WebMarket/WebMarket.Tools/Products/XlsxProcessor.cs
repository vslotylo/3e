using System;
using System.IO;
using OfficeOpenXml;

namespace WebMarket.Tools.Products
{
    public abstract class XlsxProcessor
    {
        private readonly string path;
        private ExcelPackage excelPackage;

        public XlsxProcessor(string path)
        {
            this.path = path;
        }

        public string DocumentPath
        {
            get { return this.path; }
        }

        public void Process()
        {
            using (var stream = File.Open(path, FileMode.Open, FileAccess.ReadWrite))
            {
                excelPackage = new ExcelPackage(stream);
                this.Processing(excelPackage);
            }

            this.SaveDocument();
        }

        protected int GetColumnIndexByName(ExcelWorksheet sheet, string columnName)
        {
            const int notFound = -1;
            if (sheet == null)
            {
                return notFound;
            }

            var columnIndex = 1;
            while (sheet.Cells[1, columnIndex].Value != null)
            {
                var value = sheet.Cells[1, columnIndex].Value.ToString();
                if (string.IsNullOrEmpty(value))
                {
                    columnIndex = notFound;
                    break;
                }

                if (string.Compare(value, columnName, StringComparison.InvariantCultureIgnoreCase) == 0)
                {
                    break;
                }

                columnIndex++;
            }

            return columnIndex;
        }

        protected void SaveDocument()
        {
            var file = excelPackage.GetAsByteArray();
            File.WriteAllBytes(this.DocumentPath, file);
        }

        protected virtual void BeginProcessing()
        {
        }

        protected virtual void EndProcessing()
        {
            this.SaveDocument();
        }

        protected abstract void Processing(ExcelPackage excelPackage);
    }
}
