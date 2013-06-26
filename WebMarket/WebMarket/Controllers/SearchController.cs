using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using WebMarket.Core;
using WebMarket.DAL.Entities;
using WebMarket.Filters;


namespace WebMarket.Controllers
{
    public class SearchController : ListControllerBase<Product>
    {
        private Expression<Func<Product, bool>> expression;

        public ActionResult Index(PageFilter pageFilter, SortFilter sortFilter, PageSizeFilter pageSizeFilter, SearchFilter searchFilter)
        {
            var seperators = new[] { " ", "-" };
            var tokens = searchFilter.Keyword.Split(seperators, StringSplitOptions.RemoveEmptyEntries);
            this.ViewModel = new FilterViewModelBase<Product>(pageSizeFilter, sortFilter, pageFilter, searchFilter);
            this.expression = obj=> tokens.All(t=> obj.Name.Contains(t));
            var avrs = DbContext.Avrs.Include(obj => obj.Producer).Where(this.expression);
            var batteries = DbContext.Batteries.Include(obj => obj.Producer).Where(this.expression);
            var ups = DbContext.Ups.Include(obj => obj.Producer).Where(this.expression);
            var converters = DbContext.Converters.Include(obj => obj.Producer).Where(this.expression);
            var chargers = DbContext.Chargers.Include(obj => obj.Producer).Where(this.expression);
            var voltageRelays = DbContext.VoltageRelays.Include(obj => obj.Producer).Where(this.expression);
            var currentRelays = DbContext.CurrentRelays.Include(obj => obj.Producer).Where(this.expression);
            var temperatureRelays = DbContext.TemperatureRelays.Include(obj => obj.Producer).Where(this.expression);
            var timeRelays = DbContext.TimeRelays.Include(obj => obj.Producer).Where(this.expression);
            var voltmeters = DbContext.Voltmeters.Include(obj => obj.Producer).Where(this.expression);
            
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
