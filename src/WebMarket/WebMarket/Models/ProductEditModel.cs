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
            this.DynamicPropertiesKeys = string.Join(SEPERATOR, product.DynamicProperties.Select(obj => obj.Key));
            this.DynamicPropertiesValues = string.Join(SEPERATOR, product.DynamicProperties.Select(obj => obj.Value));
        }

        public override Product ToEntity(Product original)
        {
            var entity = base.ToEntity(original);
            var dict = new Dictionary<string, string>();
            var keys = DynamicPropertiesKeys.Split(new[] { SEPERATOR }, StringSplitOptions.RemoveEmptyEntries);
            var values = DynamicPropertiesValues.Split(new[] { SEPERATOR }, StringSplitOptions.RemoveEmptyEntries);
            for (var i = 0; i < keys.Count(); i++)
            {
                dict.Add(keys[i], values[i]);
            }

            entity.Info = JsonConvert.SerializeObject(dict);
            return entity;
        }

        public string DynamicPropertiesKeys { get; set; }
        public string DynamicPropertiesValues { get; set; }
    }
}
