using System;
using System.Collections.Generic;
using WebMarket.Core;
using WebMarket.DAL.Entities;
using WebMarket.Filters;

namespace WebMarket.ViewModels
{
    public class AvrViewModel : FilterViewModelBase<Product>
    {
        public AvrViewModel(PageSizeFilter pageSizeFilter, 
            SortFilter sortFilter, 
            ProducersFilter producerFilter, 
            PageFilter pageFilter, 
            TypeFilter typeFilter, 
            PowerCapacityFilter powerCapacityFilter)
            : base(pageSizeFilter, sortFilter, producerFilter, pageFilter, typeFilter)
        {
            this.Filters.Add(powerCapacityFilter);
        }
    
        public IEnumerable<GenericFilterModel<Tuple<double, double>>> PowerCapacity { get; set; }
    }
}