using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using OfficeOpenXml;
using WebMarket.DAL.Common;
using WebMarket.DAL.Entities;
using WebMarket.DAL.Exceptions;

namespace WebMarket.DAL.Data.Import
{
    public class DataImporter
    {
        private const string EntetiesNamespace = "WebMarket.DAL.Entities.{0}";

        public void Import(Stream stream, WebMarketDbContext context)
        {
            using (stream)
            {
                try
                {
                    var excelPackage = new ExcelPackage(stream);
                    var entityImporter = new EntityImporter(context);
                    var minfoImport = entityImporter.GetType().GetMethods().FirstOrDefault(obj=>obj.Name.ToLower()=="import" && obj.IsGenericMethod);
                    var minfoSet = context.GetType().GetMethods().FirstOrDefault(obj => obj.Name.ToLower() == "set" && obj.IsGenericMethod);

                    Type typeDbSet = typeof(DbSet<>);
                    Type entityType;
                    Type genDbSet;
                    object dbSetObj;
                    var sheets =
                        excelPackage.Workbook.Worksheets.Where(obj => !obj.Name.StartsWith("_")).Select(item => item.Name);
                    foreach (var typeName in sheets)
                    {
                        entityType = Type.GetType(string.Format(EntetiesNamespace, typeName));
                        
                        if (entityType == null)
                        {
                            entityType = typeof(Product);
                        }

                        var sheet = excelPackage.Workbook.Worksheets[typeName];
                        var entitiesObj = minfoImport.MakeGenericMethod(entityType).Invoke(entityImporter, new object[] { sheet });
                        genDbSet = typeDbSet.MakeGenericType(entityType);
                        dbSetObj = minfoSet.MakeGenericMethod(entityType).Invoke(context, null);
                        
                        var dbSet = Convert.ChangeType(dbSetObj, genDbSet) as IEnumerable<dynamic>;
                        var entities = dbSet.ToList();
                        
                        var addMethodInfo = dbSet.GetType().GetMethod("Add");
                        
                        foreach (var entity in entitiesObj as IEnumerable)
                        {
                            var name = entityType.GetProperty("Name").GetValue(entity).ToString().Trim();
                            var foundEntity = entities.SingleOrDefault(obj =>
                                { 
                                    string temp = obj.Name.ToString();
                                    return string.Compare(temp, name, StringComparison.OrdinalIgnoreCase) == 0;
                                });
                            if (foundEntity == null)
                            {
                                addMethodInfo.Invoke(dbSet, new[] { entity });
                            }
                            else
                            {
                                var idProp = entityType.GetProperty("Id");
                                idProp.SetValue(entity, foundEntity.Id);
                                context.Entry(foundEntity).CurrentValues.SetValues(entity);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    throw new EntityImportException("Entity import exception occurs", e);
                }
            }
        }
    }
}
