using System.Collections.Generic;
using System.Web.Routing;
using PagedList;
using WebMarket.DAL.Entities;

namespace WebMarket.ViewModels
{
    public class PagingViewModel
    {
        public IList<RouteValueDictionary> Routes { get; set; }
        public IPagedList<Product> List { get; set; }
    }
}
