using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebMarket.Repository.Entities;

namespace WebMarket.Models
{
    public class ProducerEditModel : ProducerModel
    {
        public ProducerEditModel()
        {
            Currencies = new List<SelectListItem>
                {
                    new SelectListItem
                        {
                            Selected = false,
                            Text = "Гривня",
                            Value = "Uah"
                        },
                    new SelectListItem
                        {
                            Selected = false,
                            Text = "Долари",
                            Value = "Usd"
                        }
                };
        }

        public ProducerEditModel(Producer entity) : this()
        {
            Id = entity.Id;
            Currencies.Single(obj => obj.Value == entity.BuyCurrency.ToString()).Selected = true;
            Description = entity.Description;
            DisplayName = entity.DisplayName;
            HomePage = entity.HomePage;
            Name = entity.Name;
            UsdRate = entity.UsdRate;
        }

        public IEnumerable<SelectListItem> Currencies { get; private set; }
    }
}