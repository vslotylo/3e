using System;
using System.Collections.Generic;
using System.Linq;
using OfficeOpenXml;
using System.Reflection;
using WebMarket.DAL.Entities;
using WebMarket.DAL.Exceptions;

namespace WebMarket.DAL.Data.Import
{
    public class EntityImporter
    {
        private readonly List<Producer> producers;

        public EntityImporter(List<Producer> producers)
        {
            this.producers = producers;
        }

        // used via reflection
        public IList<T> Import<T>(ExcelPackage package) where T :new()
        {
            Type entityType = typeof(T);
            var sheet = package.Workbook.Worksheets[entityType.Name];
            int row = 1;
            int col = 1;
            var columnNames = new Dictionary<int, string>();
            var entities = new List<T>();
            
            while (sheet.Cells[row, col].Value != null)
            {
                var columnName = sheet.Cells[row, col].Value.ToString();
                columnNames.Add(col, columnName);
                col++;
            }

            col = 1;
            row++;
            while (sheet.Cells[row, col].Value != null)
            {
                var entity = new T();
                PropertyInfo[] properties = entityType.GetProperties();
                for (int i = 1; i <= columnNames.Keys.Count; i++)
                {
                    PropertyInfo property = properties.FirstOrDefault(p => string.Compare(p.Name, columnNames[i], StringComparison.OrdinalIgnoreCase) == 0);
                    if (property == null)
                    {
                        throw new EntityImportException(string.Format("Property with name {0} was not found among propertis of type {1}", columnNames[i], entityType.Name));
                    }

                    object value = sheet.Cells[row, i].Value;
                    if (value == null)
                    {
                        //TODO log warning
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

                entities.Add(entity);
                row++;
            }

            return entities;
        }
    }
}
