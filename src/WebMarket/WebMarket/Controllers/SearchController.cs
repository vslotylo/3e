using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using WebMarket.Core;
using WebMarket.DAL.Entities;
using WebMarket.Filters;


namespace WebMarket.Controllers
{
    public class SearchController : ListControllerBase
    {
        private Expression<Func<Product, bool>> expression;

        public ActionResult Index(PageFilter pageFilter, SortFilter sortFilter, PageSizeFilter pageSizeFilter, SearchFilter searchFilter)
        {
            var seperators = new[] { " ", "-" };
            var tokens = searchFilter.Keyword.Split(seperators, StringSplitOptions.RemoveEmptyEntries);
            this.ViewModel = new FilterViewModelBase<Product>(pageSizeFilter, sortFilter, pageFilter, searchFilter);
            this.expression = obj=> tokens.All(t=> obj.Name.Contains(t));
            var products = DbContext.Products.Include(obj => obj.Producer).Where(this.expression);
            this.StartInitializeCommon(products.Count());
            this.EndInitializeCommon(products);
            return this.View(this.ViewModel);
        }
    }
}
