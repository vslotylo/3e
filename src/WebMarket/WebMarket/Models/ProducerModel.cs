using System;
using System.Web.Mvc;
using WebMarket.Repository.Entities;
using WebMarket.Repository.Entities.Enums;

namespace WebMarket.Models
{
    public class ProducerModel : ModelBase<Producer>
    {
        public override Producer ToEntity(Producer original)
        {
            var entity = new Producer
            {
                Name = Name,
                Id = Id,
                Description = Description,
                DisplayName = DisplayName,
                HomePage = HomePage,
                BuyCurrency = (Currency)Enum.Parse(typeof(Currency), BuyCurrency)
            };

            if (entity.BuyCurrency == Currency.Usd)
            {
                entity.UsdRate = UsdRate;
            }

            return entity;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string HomePage { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        public string BuyCurrency { get; set; }
        public double UsdRate { get; set; }
    }
}