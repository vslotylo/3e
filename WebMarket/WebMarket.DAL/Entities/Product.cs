﻿using System;
using System.Collections.Generic;
using System.Linq;
using WebMarket.DAL.Entities.Enums;
using WebMarket.DAL.Infrustructure;
using WebMarket.DAL.Interfaces;

namespace WebMarket.DAL.Entities
{
    public class Product : IPreviewInfo
    {
        protected readonly List<ProductInfo> infos = new List<ProductInfo>();
        protected readonly List<string> types = new List<string>();

        public Product()
        {
            Photo = string.Empty;            
        }

        public string ProductId { get { return ProductManager.GetProductId(this); } }
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Controller
        {
            get
            {
                return this.GetType().Name.ToLower();
            }
        }
        public double PriceUsd
        {
            get
            {
                double usdPrice = Price;
                if (Producer.BuyCurrency == Currency.Uah)
                {
                    usdPrice = Math.Round(Price / Producer.UsdRate);
                }

                return usdPrice;
            }
        }
        public double PriceUah
        {
            get
            {
                double uahPrice = Price;
                if (Producer.BuyCurrency == (int)Currency.Usd)
                {
                    uahPrice = Math.Round(Price * Producer.UsdRate);
                }

                return uahPrice;
            }
        }

        public double Weight { get; set; }
        public double Rate { get; set; }
        public string Photo { get; set; }
        public string Description { get; set; }
        public string WorkingConditions { get; set; }
        public Producer Producer { get; set; }
        
        public string Dimension { get; set; }
        public int Warranty { get; set; }
        public int Type { get; set; }
        
        public bool IsAvailable { get; set; }
        public bool IsTopBuyed { get; set; }
        public bool IsNew { get; set; }
        public double Discount { get; set; }
        public string Info { get; set; }
        private Dictionary<string, string> parsedInfo;
        public Dictionary<string,string> ParsedInfo
        {
            get
            {
                if (parsedInfo == null)
                {
                    parsedInfo = new Dictionary<string, string>();
                    if (string.IsNullOrEmpty(Info))
                    {
                        return parsedInfo;
                    }

                    string[] items = this.Info.Split('&');
                    foreach (var item in items)
                    {
                        int index = item.IndexOf('=');
                        parsedInfo.Add(item.Substring(0, index), item.Substring(index + 1, item.Length - index -1));
                    }
                }

                return parsedInfo;
            }
        }

        public string SuppliedItems { get; set; }

        private string[] parsedSuppliedItems;
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
            string type = this.GetType().Name.ToLower();
            string extension;
            string name = GetName(splitedPhotos[0], out extension);
            return string.Format("{0}/{1}{2}", type, name, extension);
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

        protected virtual void InitializeProductInfos()
        {
            infos.Add(new ProductInfo { Name = "Модель", Value = Name });
            infos.Add(new ProductInfo { Name = "Тип", Value = TypeString });
            infos.Add(new ProductInfo { Name = "Виробник", Value = Producer.Name });                
        }

        public IList<ProductInfo> GetAllProductInfo()
        {
            if (infos.Count == 0)
            {
                InitializeProductInfos();
            }

            return infos;
        }

        public string GetProductPreviewInfo()
        {
            return string.Join(" | ", GetAllProductInfo().Where(i => i.IsPreview).Select(i => string.Format("{0}: {1}", i.Name, i.Value)));
        }

        public string TypeString
        {
            get
            {
                return types[this.Type];
            }
        }
    }
}