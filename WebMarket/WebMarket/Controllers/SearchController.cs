using System.Collections.Generic;
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
            var avrs = DbContext.Avrs.Include(obj => obj.Producer).Where(obj => obj.Name.Contains(searchFilter.Keyword));
            var batteries = DbContext.Batteries.Include(obj => obj.Producer).Where(obj => obj.Name.Contains(searchFilter.Keyword));
            var ups = DbContext.Ups.Include(obj => obj.Producer).Where(obj => obj.Name.Contains(searchFilter.Keyword));
            var converters = DbContext.Converters.Include(obj => obj.Producer).Where(obj => obj.Name.Contains(searchFilter.Keyword));
            var chargers = DbContext.Chargers.Include(obj => obj.Producer).Where(obj => obj.Name.Contains(searchFilter.Keyword));
            var voltageRelays = DbContext.VoltageRelays.Include(obj => obj.Producer).Where(obj => obj.Name.Contains(searchFilter.Keyword));
            var currentRelays = DbContext.CurrentRelays.Include(obj => obj.Producer).Where(obj => obj.Name.Contains(searchFilter.Keyword));
            var temperatureRelays = DbContext.TemperatureRelays.Include(obj => obj.Producer).Where(obj => obj.Name.Contains(searchFilter.Keyword));
            var timeRelays = DbContext.TimeRelays.Include(obj => obj.Producer).Where(obj => obj.Name.Contains(searchFilter.Keyword));
            var voltmeters = DbContext.Voltmeters.Include(obj => obj.Producer).Where(obj => obj.Name.Contains(searchFilter.Keyword));
            
            var result = new List<Product>();
            result.AddRange(avrs);
            result.AddRange(batteries);
            result.AddRange(ups);
            result.AddRange(converters);
            result.AddRange(chargers);
            result.AddRange(voltageRelays);
            result.AddRange(currentRelays);
            result.AddRange(temperatureRelays);
            result.AddRange(timeRelays);
            result.AddRange(voltmeters);

            this.StartInitializeCommon(result.Count());
            this.EndInitializeCommon(result);
            return this.View(this.ViewModel);
        }
    }
}
