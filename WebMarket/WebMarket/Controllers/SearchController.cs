using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using WebMarket.Core;
using WebMarket.DAL.Entities;
using WebMarket.Filters;


namespace WebMarket.Controllers
{
    public class SearchController : ListControllerBase<Product>
    {
        public ActionResult Index(PageFilter pageFilter, SortFilter sortFilter, PageSizeFilter pageSizeFilter, SearchFilter searchFilter)
        {
            //var seperators = new[] { " ", "-" };
            //var tokens = keyword.Split(seperators, StringSplitOptions.RemoveEmptyEntries);
            this.ViewModel = new FilterViewModelBase<Product>(pageSizeFilter, sortFilter, pageFilter, searchFilter);
            var entities = DbContext.Avrs.Include(obj => obj.Producer).Where(obj => obj.Name.Contains(searchFilter.Keyword));
            this.StartInitializeCommon(entities.Count());
            this.EndInitialize(entities);
            return this.View(this.ViewModel);
        }
    }
}
