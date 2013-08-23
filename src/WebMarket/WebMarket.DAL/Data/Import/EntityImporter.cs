using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using OfficeOpenXml;
using System.Reflection;
using WebMarket.DAL.Common;
using WebMarket.DAL.Entities;
using WebMarket.DAL.Exceptions;

namespace WebMarket.DAL.Data.Import
{
    public class EntityImporter
    {
        private readonly List<Producer> producers;
        private readonly List<Category> categories;

        public EntityImporter(WebMarketDbContext context)
        {
            this.producers = context.Producers.ToList();
            this.categories = context.Categories.ToList();
        }

        // used via reflection
        public IList<T> Import<T>(ExcelWorksheet excelWorksheet) where T : new()
        {
            Type entityType = typeof(T);
            int row = 1;
            int col = 1;
            var columnNames = new Dictionary<int, string>();
            var entities = new List<T>();
            
            while (excelWorksheet.Cells[row, col].Value != null)
            {
                var columnName = excelWorksheet.Cells[row, col].Value.ToString();
                columnNames.Add(col, columnName);
                col++;
            }

            col = 1;
            row++;
            var isProduct = entityType == typeof(Product);
            if (isProduct)
            {
                row++;
            }

            while (excelWorksheet.Cells[row, col].Value != null)
            {
                var entity = new T();
                PropertyInfo[] properties = entityType.GetProperties();
                var dynaicProperties = new Dictionary<string, string>();
                for (int i = 1; i <= columnNames.Keys.Count; i++)
                {
                    PropertyInfo property = properties.FirstOrDefault(p => string.Compare(p.Name, columnNames[i], StringComparison.OrdinalIgnoreCase) == 0);
                    if (property == null)
                    {
                        if (!isProduct)
                        {
                            throw new EntityImportException(string.Format("Property with name {0} was not found among properties of type {1}", columnNames[i], entityType.Name));
                        }
                    }

                    object value = excelWorksheet.Cells[row, i].Value;
                    if (value == null)
                    {
                        //TODO log warning
                        continue;
                    }

                    if (isProduct && property == null)
                    {
                        var suffix = excelWorksheet.Cells[2, i].Value ?? string.Empty;
                        dynaicProperties[columnNames[i]] = string.Format("{0} {1}", value.ToString().Trim(), suffix.ToString().Trim());
                        continue;
                    }

                    Type propertyType = property.PropertyType;
                    object propertyValue = value;
                    if (propertyType.IsSubclassOf(typeof(Enum)))
                    {
                        propertyValue = Enum.Parse(propertyType, propertyValue.ToString());
                    }
                    else if (propertyType == typeof(int))
                    {
                        propertyValue = int.Parse(value.ToString());
                    }
                    else if (propertyType == typeof(string))
                    {
                        propertyValue = value.ToString().Trim();
                    }
                    else if (propertyType == typeof(Producer))
                    {
                        string valueStr = value.ToString();
                        propertyValue = this.producers.FirstOrDefault(p => string.Compare(p.Name, valueStr, StringComparison.OrdinalIgnoreCase) == 0);
                    }

                    property.SetValue(entity, propertyValue, null);
                }

                if (entityType == typeof(Producer))
                {
                    producers.Add(entity as Producer);
                }
                if (isProduct)
                {
                    string valueStr = excelWorksheet.Name;
                    var category = this.categories.FirstOrDefault(p => string.Compare(p.Name, valueStr, StringComparison.OrdinalIgnoreCase) == 0);
                    var categoryProp = properties.FirstOrDefault(obj => obj.Name == "Category");
                    categoryProp.SetValue(entity, category, null);

                    var categoryNameProp = properties.FirstOrDefault(obj => obj.Name == "CategoryName");
                    categoryNameProp.SetValue(entity, valueStr, null);

                    var infoProp = properties.SingleOrDefault(p => p.Name == "Info");
                    var serializer = new JavaScriptSerializer();
                    infoProp.SetValue(entity, serializer.Serialize(dynaicProperties), null);
                }

                entities.Add(entity);
                row++;
            }

            return entities;
        }
    }
}
