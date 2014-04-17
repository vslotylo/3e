using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using WebMarket.Repository.Entities;

namespace WebMarket.Models
{
    public class ProductEditModel : ProductModel
    {
        private const string SEPERATOR = "\r\n";

        public ProductEditModel()
        {
        }

        public ProductEditModel(Product product)
            : base(product)
        {
            DynamicPropertiesKeys = string.Join(SEPERATOR, product.DynamicProperties.Select(obj => obj.Key));
            DynamicPropertiesValues = string.Join(SEPERATOR, product.DynamicProperties.Select(obj => obj.Value));
        }

        public string DynamicPropertiesKeys { get; set; }
        public string DynamicPropertiesValues { get; set; }

        public override Product ToEntity(Product original)
        {
            Product entity = base.ToEntity(original);
            var dict = new Dictionary<string, string>();
            string[] keys = DynamicPropertiesKeys.Split(new[] {SEPERATOR}, StringSplitOptions.RemoveEmptyEntries);
            string[] values = DynamicPropertiesValues.Split(new[] {SEPERATOR}, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < keys.Count(); i++)
            {
                if (!string.IsNullOrEmpty(keys[i]) && !string.IsNullOrEmpty(values[i]))
                {
                    dict.Add(keys[i].Trim(), values[i].Trim());
                }
            }

            entity.Info = JsonConvert.SerializeObject(dict);
            return entity;
        }
    }
}