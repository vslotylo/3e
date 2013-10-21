using System.Collections.Generic;
using WebMarket.Common;
using WebMarket.DAL.Entities;
using WebMarket.Filters;

namespace WebMarket.Core
{
    public class FilterViewModelBase<T> where T : Product
    {
        public FilterViewModelBase(PageSizeFilter pageSizeFilter, SortFilter sortFilter, ProducersFilter producersFilter, PageFilter pageFilter, GroupFilter groupFilter)
            : this(pageSizeFilter, sortFilter, pageFilter)
        {
            this.ProducersFilter = producersFilter;
            this.GroupsFilter = groupFilter;
            this.Filters.Add(producersFilter);
            this.Filters.Add(groupFilter);
        }

        public FilterViewModelBase(PageSizeFilter pageSizeFilter, SortFilter sortFilter, PageFilter pageFilter, SearchFilter searchFilter)
            :this(pageSizeFilter, sortFilter, pageFilter)
        {
            this.SearchFilter = searchFilter;
            this.Filters.Add(searchFilter);
        }

        private FilterViewModelBase(PageSizeFilter pageSizeFilter, SortFilter sortFilter, PageFilter pageFilter)
        {
            this.PageSizeFilter = pageSizeFilter;
            this.SortFilter = sortFilter;
            this.PageFilter = pageFilter;

            this.Filters = new FilterList { pageSizeFilter, sortFilter, pageFilter };

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
        public IEnumerable<GenericFilterModel<string>> Groups { get; set; }
        public IList<GenericFilterModelBase<int>> PageSize { get; private set; }
        public IList<GenericFilterModelBase<int>> Sort { get; private set; }

        public PagingViewModel Pagging { get; private set; }        
        public FilterList Filters { get; private set; }

        public PageSizeFilter PageSizeFilter { get; private set; }
        public SortFilter SortFilter { get; private set; }
        public ProducersFilter ProducersFilter { get; private set; }
        public PageFilter PageFilter { get; private set; }
        public GroupFilter GroupsFilter { get; private set; }
        public SearchFilter SearchFilter { get; private set; }
        public int Count { get; set; }
        public Metadata Metadata { get; set; }
    }
}