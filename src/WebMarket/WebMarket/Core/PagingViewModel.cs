using System.Collections.Generic;
using System.Web.Routing;
using PagedList;
using WebMarket.DAL.Entities;

namespace WebMarket.Core
{
    public class PagingViewModel
    {
        public PagingViewModel(string category)
        {
            this.Category = category;
        }
        public IList<RouteValueDictionary> Routes { get; set; }
        public IPagedList<Product> List { get; set; }
        public string Category { get; private set; }
    }
}
