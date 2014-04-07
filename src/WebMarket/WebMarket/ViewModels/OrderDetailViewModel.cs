using System.Collections.Generic;
using System.Web.Mvc;
using WebMarket.Repository.Entities;

namespace WebMarket.ViewModels
{
    public class OrderDetailViewModel : Controller
    {
        public OrderDetailViewModel(Order order, IEnumerable<Product> products)
        {
            this.Order = order;
            this.Products = products;
        }

        public Order Order { get; set; }
        public IEnumerable<Product> Products { get; set; }

    }
}
