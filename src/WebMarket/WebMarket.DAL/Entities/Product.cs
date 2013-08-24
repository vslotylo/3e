using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using WebMarket.DAL.Entities.Enums;
using WebMarket.DAL.Infrustructure;
using WebMarket.DAL.Interfaces;

namespace WebMarket.DAL.Entities
{
    public class Product : IPreviewInfo
    {
        private readonly Lazy<List<ProductInfo>> infos;
        private readonly Lazy<Price> price;
        private readonly Lazy<List<ProductInfo>> dynamicProperties;
        private static readonly JavaScriptSerializer Serializer = new JavaScriptSerializer();
        private string[] parsedSuppliedItems;

        public Product()
        {
            this.Photo = string.Empty;
            this.price = new Lazy<Price>(this.InitPrice);
            this.infos = new Lazy<List<ProductInfo>>(this.InitInfos);
            this.dynamicProperties = new Lazy<List<ProductInfo>>(this.InitDynamicProperties);
        }

        public int Id { get; set; }
        public string DisplayName { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public double Weight { get; set; }
        public double Rate { get; set; }
        public string Photo { get; set; }
        public string Description { get; set; }
        public string WorkingConditions { get; set; }

        public string SuppliedItems { get; set; }
        public string Dimension { get; set; }
        public int Warranty { get; set; }
        public Producer Producer { get; set; }
        public Category Category { get; set; }
        public string CategoryName { get; set; }
        public SubCategory SubCategory { get; set; }
        public string SubCategoryName { get; set; }
        public double Discount { get; set; }
        public string Info { get; set; }

        public Availability Availability { get; set; }
        public DisplayClass DisplayClass { get; set; }

        public string ProductId
        {
            get
            {
                return string.Format("{0:000-000}", Id);
            }
        }

        public Price CalculatedPrice
        {
            get
            {
                return price.Value;
            }
        }

        public IEnumerable<ProductInfo> DynamicProperties
        {
            get
            {
                return this.dynamicProperties.Value;
            }
        }

        public string[] ParsedSuppliedItems
        {
            get
            {
                if (parsedSuppliedItems == null)
                {
                    if (!string.IsNullOrEmpty(this.SuppliedItems))
                    {
                        parsedSuppliedItems = this.SuppliedItems.Split(',');
                    }
                    else
                    {
                        return new string[0];
                    }
                }

                return parsedSuppliedItems;
            }
        }

        public IEnumerable<ProductInfo> ProductInfo
        {
            get
            {
                return this.infos.Value;
            }
        }

        private List<ProductInfo> InitDynamicProperties()
        {
            var dict = Serializer.Deserialize<Dictionary<string, string>>(this.Info);
            var typedProp = new List<ProductInfo>();
            foreach (var item in dict)
            {
                bool result;
                if (bool.TryParse(item.Value, out result))
                {
                    typedProp.Add(new ProductInfo(item.Key, result, true));
                }
                else
                {
                    typedProp.Add(new ProductInfo(item.Key, item.Value));
                }
            }

            return typedProp;
        }

        private Price InitPrice()
        {
            return new Price(this);
        }

        private List<ProductInfo> InitInfos()
        {
            var list = new List<ProductInfo>
                           {
                               new ProductInfo("Модель", this.DisplayName),
                               new ProductInfo("Тип", this.SubCategoryName),
                               new ProductInfo("Виробник", this.Producer.Name)
                           };
            return list;
        }

        public Dictionary<string, string> GetPhotos()
        {
            var dict = new Dictionary<string, string>();
            var splitedPhotos = Photo.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            if (splitedPhotos.Length != 2)
            {
                return dict;
            }

            string extension;
            var justName = GetName(splitedPhotos[0], out extension);

            int count = int.Parse(splitedPhotos[1]);

            for (int i = 1; i <= count; i++)
            {
                dict[string.Format("{0}.{1}thmb{2}", justName, i, extension)] = string.Format("{0}.{1}{2}", justName, i, extension);
            }

            return dict;
        }

        private static string GetName(string name, out string extension)
        {
            int index = name.LastIndexOf('.');
            string justName;
            if (index == -1)
            {
                justName = name;
                extension = ".jpg";
            }
            else
            {
                justName = name.Substring(0, index);
                extension = name.Substring(index, name.Length - index);
            }

            return justName;
        }

        public string GetListPhoto()
        {
            if (string.IsNullOrEmpty(Photo))
            {
                return string.Empty;
            }

            var splitedPhotos = Photo.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            string extension;
            string name = GetName(splitedPhotos[0], out extension);
            return string.Format("{0}/{1}{2}", CategoryName.ToLower(), name, extension);
        }

        public string GetPreview()
        {
            if (string.IsNullOrEmpty(Photo))
            {
                return string.Empty;
            }

            var splitedPhotos = Photo.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            string extension;
            string name = GetName(splitedPhotos[0], out extension);
            return string.Format("{0}{1}", name, extension);
        }

        public string GetProductPreviewInfo()
        {
            return string.Join(" | ", this.DynamicProperties.Where(obj=>!obj.IsBool).Take(6).Select(i => string.Format("{0}: {1}", i.Key, i.Value)));
        }
    }
}
