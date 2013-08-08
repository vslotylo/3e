using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using OfficeOpenXml;
using WebMarket.DAL.Common;
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
                    var excelDocument = new ExcelPackage(stream);
                    var entityImporter = new EntityImporter(context.Producers.ToList());
                    var minfoImport = entityImporter.GetType().GetMethods().FirstOrDefault(obj=>obj.Name.ToLower()=="import" && obj.IsGenericMethod);
                    var minfoSet = context.GetType().GetMethods().FirstOrDefault(obj => obj.Name.ToLower() == "set" && obj.IsGenericMethod);

                    Type typeDbSet = typeof(DbSet<>);
                    Type typeOfEntity;
                    Type genDbSet;
                    object dbSetObj;
                    var sheets =
                        excelDocument.Workbook.Worksheets.Where(
                            obj => string.Compare(obj.Name, "metadata", StringComparison.InvariantCultureIgnoreCase) != 0).Select(item => item.Name);
                    foreach (var typeName in sheets)
                    {
                        typeOfEntity = Type.GetType(string.Format(EntetiesNamespace, typeName));
                        if (typeOfEntity == null)
                        {
                            throw new EntityImportException(string.Format("Could not find type for entity \"{0}\"", typeName));
                        }

                        var entitiesObj = minfoImport.MakeGenericMethod(typeOfEntity).Invoke(entityImporter, new object[] { excelDocument });
                        genDbSet = typeDbSet.MakeGenericType(typeOfEntity);
                        dbSetObj = minfoSet.MakeGenericMethod(typeOfEntity).Invoke(context, null);
                        
                        var dbSet = Convert.ChangeType(dbSetObj, genDbSet) as IEnumerable<dynamic>;
                        var entities = dbSet.ToList();
                        
                        var addMethodInfo = dbSet.GetType().GetMethod("Add");
                        
                        foreach (var entity in entitiesObj as IEnumerable)
                        {
                            var name = typeOfEntity.GetProperty("Name").GetValue(entity).ToString().Trim();
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
                                var idProp = typeOfEntity.GetProperty("Id");
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
