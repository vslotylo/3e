using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using WebMarket.Repository.Entities.Enums;
using WebMarket.Repository.Infrustructure;
using WebMarket.Repository.Interfaces;

namespace WebMarket.Repository.Entities
{
    public class Product : IPreviewInfo
    {
        private readonly Lazy<List<ProductInfo>> infos;
        private readonly Lazy<Price> price;
        private IEnumerable<ProductInfo> dynamicProperties;

        public Product()
        {
            Photo = string.Empty;
            price = new Lazy<Price>(InitPrice);
            infos = new Lazy<List<ProductInfo>>(InitInfos);
        }

        public int Id { get; set; }
        public string DisplayName { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public double Weight { get; set; }
        public double Rate { get; set; }
        public string Photo { get; set; }

        [AllowHtml]
        public string Description { get; set; }
        [AllowHtml]
        public string DescriptionRU { get; set; }

        public string WorkingConditions { get; set; }
        public string SuppliedItems { get; set; }
        public string Dimension { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public int Warranty { get; set; }
        public Producer Producer { get; set; }

        [ForeignKey("CategoryName")]
        public Category Category { get; set; }

        [Required, MaxLength(128)]
        public string CategoryName { get; set; }

        [ForeignKey("GroupName")]
        public Group Group { get; set; }

        [Required]
        public string GroupName { get; set; }

        public double Discount { get; set; }
        public string Info { get; set; }

        public Availability Availability { get; set; }
        public DisplayClass DisplayClass { get; set; }

        public string ProductId
        {
            get { return string.Format("{0:000-000}", Id); }
        }

        public Price CalculatedPrice
        {
            get { return price.Value; }
        }

        public IEnumerable<ProductInfo> DynamicProperties
        {
            get
            {
                if (dynamicProperties == null)
                {
                  if (Info == null)
                  {
                    dynamicProperties = new List<ProductInfo>();
                  }
                  else
                  {
                    var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(Info);
                    dynamicProperties = dict.Select(item => new ProductInfo(item.Key, item.Value)).ToList();
                  }
                }

                return dynamicProperties;
            }
            set
            {
                dynamicProperties = value;
                var dict = dynamicProperties.ToDictionary(obj => obj.Key, obj => obj.Value);
                Info = JsonConvert.SerializeObject(dict);
            }
        }

        public IEnumerable<ProductInfo> ProductInfo
        {
            get { return infos.Value; }
        }

        public Dictionary<string, string> GetPhotos()
        {
            var dict = new Dictionary<string, string>();
            string[] splitedPhotos = Photo.Split(new[] {","}, StringSplitOptions.RemoveEmptyEntries);
            if (splitedPhotos.Length != 2)
            {
                return dict;
            }

            string extension;
            string justName = GetName(splitedPhotos[0], out extension);

            int count = int.Parse(splitedPhotos[1]);

            for (int i = 1; i <= count; i++)
            {
                dict[string.Format("{0}.{1}thmb{2}", justName, i, extension)] = string.Format("{0}.{1}{2}", justName, i,
                                                                                              extension);
            }

            return dict;
        }

        public string GetListPhoto()
        {
            if (string.IsNullOrEmpty(Photo))
            {
                return string.Empty;
            }

            string[] splitedPhotos = Photo.Split(new[] {","}, StringSplitOptions.RemoveEmptyEntries);
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

            string[] splitedPhotos = Photo.Split(new[] {","}, StringSplitOptions.RemoveEmptyEntries);
            string extension;
            string name = GetName(splitedPhotos[0], out extension);
            return string.Format("{0}{1}", name, extension);
        }

        private Price InitPrice()
        {
            return new Price(this);
        }

        private List<ProductInfo> InitInfos()
        {
            var list = new List<ProductInfo>
                {
                    new ProductInfo("Модель", DisplayName),
                    new ProductInfo("Тип", Group.DisplayName)
                };
            return list;
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

        public string GetProductPreviewInfo()
        {
            return string.Join(" | ",
                               DynamicProperties.Where(obj => !obj.IsBool)
                                                .Take(6)
                                                .Select(i => string.Format("{0}: {1}", i.Key, i.Value)));
        }

        public IEnumerable<string> GetParsedSuppliedItems(Category category)
        {
            return JsonConvert.DeserializeObject<string[]>(SuppliedItems)
                              .Select(obj => obj.Replace("{titledetails}", category.TitleDetails))
                              .Select(obj => obj.Replace("{displayname}", DisplayName));
        }
    }
}