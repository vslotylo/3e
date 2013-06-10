using System.Collections.Generic;
using WebMarket.Common;
using WebMarket.DAL.Entities;
using WebMarket.Filters;

namespace WebMarket.Core
{
    public class FilterViewModelBase<T> where T : Product
    {
        public FilterViewModelBase(PageSizeFilter pageSizeFilter, SortFilter sortFilter, ProducersFilter producersFilter, PageFilter pageFilter, TypeFilter typeFilter)
        {
            this.Filters = new FilterList { pageSizeFilter, sortFilter, producersFilter, pageFilter, typeFilter };

            this.PageSizeFilter = pageSizeFilter;
            this.SortFilter = sortFilter;
            this.ProducersFilter = producersFilter;
            this.PageFilter = pageFilter;
            this.TypesFilter = typeFilter;

            this.PageSize = new List<GenericFilterModelBase<int>>
                                {
                                    new GenericFilterModelBase<int> { Value = Constants.PageSizeDefault },
                                    new GenericFilterModelBase<int> { Value = Constants.PageSizeSmall },
                                    new GenericFilterModelBase<int> { Value = Constants.PageSizeBig }
                                };

            this.Sort = new List<GenericFilterModelBase<int>>
                            {
                                new GenericFilterModelBase<int>
                                    {
                                        Value = (int)(Core.Sort.PriceAsc),
                                        Name = Constants.PriceAsc
                                    },
                                new GenericFilterModelBase<int>
                                    {
                                        Value = (int)(Core.Sort.PriceDesc),
                                        Name = Constants.PriceDesc
                                    },
                                new GenericFilterModelBase<int>
                                    {
                                        Value = (int)(Core.Sort.RateDesc),
                                        Name = Constants.RateDesc
                                    }
                            };
            this.Pagging = new PagingViewModel();
        }

        public IEnumerable<GenericFilterModel<string>> Producers { get; set; }
        public IEnumerable<GenericFilterModel<int>> Types { get; set; }
        public IList<GenericFilterModelBase<int>> PageSize { get; set; }
        public IList<GenericFilterModelBase<int>> Sort { get; set; }

        public PagingViewModel Pagging { get; set; }        
        public FilterList Filters { get; set; }

        public PageSizeFilter PageSizeFilter { get; private set; }
        public SortFilter SortFilter { get; private set; }
        public ProducersFilter ProducersFilter { get; private set; }
        public PageFilter PageFilter { get; private set; }
        public TypeFilter TypesFilter { get; private set; }


        public int Count { get; set; }        
    }
}