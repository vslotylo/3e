using System.Collections.Generic;
using System.Linq;
using WebMarket.Controllers;
using WebMarket.DAL.Entities;
using System.Web.Routing;
using WebMarket.Helpers;

namespace WebMarket.Core
{
    public abstract class ListControllerBase<T> : ControllerBase where T : Product
    {
        public FilterViewModelBase<T> ViewModel { get; set; }

        protected void InitializePager(IQueryable<T> entities)
        {
            this.ViewModel.Pagging.List = SortingHelper.ToSortedPagedList<T>(entities, (Sort)this.ViewModel.SortFilter.Sort, this.ViewModel.PageSizeFilter.PageSize, this.ViewModel.PageFilter.Page);
            this.InitializeRoutes();
        }

        protected void InitializePager(IEnumerable<T> entities)
        {
            this.ViewModel.Pagging.List = SortingHelper.ToSortedPagedList<T>(entities, (Sort)this.ViewModel.SortFilter.Sort, this.ViewModel.PageSizeFilter.PageSize, this.ViewModel.PageFilter.Page);
            this.InitializeRoutes();
        }

        private void InitializeRoutes()
        {
            IList<RouteValueDictionary> pagingRoutes = new List<RouteValueDictionary>();
            if (this.ViewModel.Pagging.List.HasPreviousPage)
            {
                pagingRoutes.Add(this.ViewModel.Filters.UpdateFilter(this.ViewModel.PageFilter, this.ViewModel.Pagging.List.PageNumber - 1));
            }
            else
            {
                pagingRoutes.Add(this.ViewModel.Filters.UpdateFilter(this.ViewModel.PageFilter, this.ViewModel.Pagging.List.PageNumber));
            }
            for (int i = 1; i <= this.ViewModel.Pagging.List.PageCount; i++)
            {
                pagingRoutes.Add(this.ViewModel.Filters.UpdateFilter(this.ViewModel.PageFilter, i));
            }
            if (this.ViewModel.Pagging.List.HasNextPage)
            {
                pagingRoutes.Add(this.ViewModel.Filters.UpdateFilter(this.ViewModel.PageFilter, this.ViewModel.Pagging.List.PageNumber + 1));
            }
            else
            {
                pagingRoutes.Add(this.ViewModel.Filters.UpdateFilter(this.ViewModel.PageFilter, this.ViewModel.Pagging.List.PageCount));
            }

            this.ViewModel.Pagging.Routes = pagingRoutes;
        }

        protected void InitializeSelectedProducer()
        {
            if (this.ViewModel.ProducersFilter == null)
            {
                return;
            }

            // todo
            foreach (var item in this.ViewModel.ProducersFilter.ProducersList)
            {
                this.ViewModel.ProducersFilter.Routes.Add(this.ViewModel.Filters.RemoveFilterPart(this.ViewModel.ProducersFilter, item));
            }
        }

        protected void InitializeSelectedTypes()
        {
            if (this.ViewModel.TypesFilter == null)
            {
                return;
            }

            // todo
            foreach (var item in this.ViewModel.TypesFilter.TypeList)
            {
                this.ViewModel.TypesFilter.Routes.Add(this.ViewModel.Filters.RemoveFilterPart(this.ViewModel.TypesFilter, item.ToString()));
                this.ViewModel.TypesFilter.DisplayList.Add(this.ViewModel.Types.Single(i=>i.Value == item).Name);
            }
        }

        protected void InitializePageSizeFilter()
        {
            foreach (var item in this.ViewModel.PageSize)
            {
                item.Routes = this.ViewModel.Filters.UpdateFilter(this.ViewModel.PageSizeFilter, item.Value);
            }
        }

        protected void InitializeSortFilter()
        {
            foreach (var item in this.ViewModel.Sort)
            {
                item.Routes = this.ViewModel.Filters.UpdateFilter(this.ViewModel.SortFilter, item.Value);
            }
        }

        protected void InitializeProducerFilter()
        {
            if (this.ViewModel.ProducersFilter == null)
            {
                return;
            }

            foreach (var item in this.ViewModel.Producers)
            {
                item.Routes = this.ViewModel.Filters.UpdateFilter(this.ViewModel.ProducersFilter, item.Value);
            }
        }

        protected void InitializeTypeFilter()
        {
            if (this.ViewModel.TypesFilter == null)
            {
                return;
            }

            foreach (var item in this.ViewModel.Types)
            {
                item.Routes = this.ViewModel.Filters.UpdateFilter(this.ViewModel.TypesFilter, item.Value);                
            }
        }

        protected void EndInitialize(IQueryable<T> entities)
        {
            this.InitializePageSizeFilter();
            this.InitializeSortFilter();
            this.InitializeProducerFilter();
            this.InitializeTypeFilter();

            this.InitializePager(entities);
            this.InitializeSelectedProducer();
            this.InitializeSelectedTypes();
        }

        protected void EndInitializeCommon(IEnumerable<T> entities)
        {
            this.InitializePageSizeFilter();
            this.InitializeSortFilter();
            this.InitializeProducerFilter();
            this.InitializeTypeFilter();

            this.InitializePager(entities);
            this.InitializeSelectedProducer();
            this.InitializeSelectedTypes();
        }

        private void InitializeSort()
        {
            this.ViewModel.Sort.FirstOrDefault(p => p.Value == this.ViewModel.SortFilter.Sort).IsSelected = true;
        }

        private void InitializePageSize()
        {
            this.ViewModel.PageSize.FirstOrDefault(p => p.Value == this.ViewModel.PageSizeFilter.PageSize).IsSelected = true;
        }

        private void InitializeProducerFilter(IQueryable<T> entities)
        {
            if (this.ViewModel.ProducersFilter == null)
            {
                return;
            }

            var producers = entities.GroupBy(p => p.Producer).Select(g => new GenericFilterModel<string> { Value = g.Key.Name, Name = g.Key.Name, ProductsCount = g.Count() }).ToList();
            foreach (var item in producers)
            {
                item.IsSelected = this.ViewModel.ProducersFilter.ProducersList.Contains(item.Value);
            }

            this.ViewModel.Producers = producers;
        }


        protected void StartInitializeCommon(int count)
        {
            this.ViewModel.Count = count;
            this.InitializeSort();
            this.InitializePageSize();
        }

        public IQueryable<T> StartInitialize(IQueryable<T> entities)
        {
            this.StartInitializeCommon(entities.Count());
            this.InitializeProducerFilter(entities);
            this.InitializeTypesFilter(entities);

            if (this.ViewModel.TypesFilter != null && this.ViewModel.TypesFilter.TypeList.Any())
            {
                entities = entities.Where(o => this.ViewModel.TypesFilter.TypeList.Contains(o.Type));
            }

            if (this.ViewModel.ProducersFilter != null && this.ViewModel.ProducersFilter.ProducersList.Any())
            {
                entities = entities.Where(o => this.ViewModel.ProducersFilter.ProducersList.Contains(o.Producer.Name));
            }


            return entities;
        }

        private void InitializeTypesFilter(IQueryable<T> entities)
        {
            if (ViewModel.TypesFilter == null)
            {
                return;
            }

            var types =
                entities.GroupBy(item => item.Type)
                        .ToList()
                        .Select(g => new GenericFilterModel<int> { Value = g.Key, Name = g.FirstOrDefault().TypeString, ProductsCount = g.Count() })
                        .ToList();
            foreach (var item in types)
            {
                item.IsSelected = this.ViewModel.TypesFilter.TypeList.Contains(item.Value);
            }

            this.ViewModel.Types = types;
        }
    }
}