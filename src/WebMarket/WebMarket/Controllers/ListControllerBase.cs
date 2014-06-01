using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using WebMarket.Core;
using WebMarket.Helpers;
using WebMarket.Repository.Current;
using WebMarket.Repository.Entities;
using WebMarket.Repository.Interfaces;

namespace WebMarket.Controllers
{
    public abstract class ListControllerBase : ControllerBase
    {
        public FilterViewModelBase ViewModel { get; set; }

        [Dependency]
        public IGroupRepository GroupRepository { get; set; }

        protected void InitializePager(IQueryable<Product> entities)
        {
            ViewModel.Pagging.List = SortingHelper.ToSortedPagedList(entities, (Sort) ViewModel.SortFilter.Sort,
                                                                     ViewModel.PageSizeFilter.PageSize,
                                                                     ViewModel.PageFilter.Page);
            InitializeRoutes();
        }

        protected void InitializePager(IEnumerable<Product> entities)
        {
            ViewModel.Pagging.List = SortingHelper.ToSortedPagedList(entities, (Sort) ViewModel.SortFilter.Sort,
                                                                     ViewModel.PageSizeFilter.PageSize,
                                                                     ViewModel.PageFilter.Page);
            InitializeRoutes();
        }

        private void InitializeRoutes()
        {
            IList<RouteValueDictionary> pagingRoutes = new List<RouteValueDictionary>();
            if (ViewModel.Pagging.List.HasPreviousPage)
            {
                pagingRoutes.Add(ViewModel.Filters.UpdateFilter(ViewModel.PageFilter,
                                                                ViewModel.Pagging.List.PageNumber - 1));
            }
            else
            {
                pagingRoutes.Add(ViewModel.Filters.UpdateFilter(ViewModel.PageFilter, ViewModel.Pagging.List.PageNumber));
            }
            for (int i = 1; i <= ViewModel.Pagging.List.PageCount; i++)
            {
                pagingRoutes.Add(ViewModel.Filters.UpdateFilter(ViewModel.PageFilter, i));
            }
            if (ViewModel.Pagging.List.HasNextPage)
            {
                pagingRoutes.Add(ViewModel.Filters.UpdateFilter(ViewModel.PageFilter,
                                                                ViewModel.Pagging.List.PageNumber + 1));
            }
            else
            {
                pagingRoutes.Add(ViewModel.Filters.UpdateFilter(ViewModel.PageFilter, ViewModel.Pagging.List.PageCount));
            }

            ViewModel.Pagging.Routes = pagingRoutes;
        }

        protected void InitializeSelectedProducer()
        {
            if (ViewModel.ProducersFilter == null)
            {
                return;
            }

            // todo
            foreach (string item in ViewModel.ProducersFilter.ProducersList)
            {
                ViewModel.ProducersFilter.Routes.Add(ViewModel.Filters.RemoveFilterPart(ViewModel.ProducersFilter, item));
                ViewModel.ProducersFilter.DisplayList.Add(
                    ViewModel.Producers.Single(
                        obj => string.Compare(obj.Value, item, StringComparison.InvariantCultureIgnoreCase) == 0).Name);
            }
        }

        protected void InitializeSelectedGroups()
        {
            if (ViewModel.GroupsFilter == null)
            {
                return;
            }

            // todo
            foreach (string item in ViewModel.GroupsFilter.GroupList)
            {
                ViewModel.GroupsFilter.Routes.Add(ViewModel.Filters.RemoveFilterPart(ViewModel.GroupsFilter, item));
                ViewModel.GroupsFilter.DisplayList.Add(
                    ViewModel.Groups.Single(
                        obj => string.Compare(obj.Value, item, StringComparison.InvariantCultureIgnoreCase) == 0).Name);
            }
        }

        protected void InitializePageSizeFilter()
        {
            foreach (var item in ViewModel.PageSize)
            {
                item.Routes = ViewModel.Filters.UpdateFilter(ViewModel.PageSizeFilter, item.Value);
            }
        }

        protected void InitializeSortFilter()
        {
            foreach (var item in ViewModel.Sort)
            {
                item.Routes = ViewModel.Filters.UpdateFilter(ViewModel.SortFilter, item.Value);
            }
        }

        protected void InitializeProducerFilter()
        {
            if (ViewModel.ProducersFilter == null)
            {
                return;
            }

            foreach (var item in ViewModel.Producers)
            {
                item.Routes = ViewModel.Filters.UpdateFilter(ViewModel.ProducersFilter, item.Value);
            }
        }

        protected void InitializeGroupFilter()
        {
            if (ViewModel.GroupsFilter == null)
            {
                return;
            }

            foreach (var item in ViewModel.Groups)
            {
                item.Routes = ViewModel.Filters.UpdateFilter(ViewModel.GroupsFilter, item.Value);
            }
        }

        protected void EndInitialize(IQueryable<Product> entities)
        {
            InitializePageSizeFilter();
            InitializeSortFilter();
            InitializeProducerFilter();
            InitializeGroupFilter();

            InitializePager(entities);
            InitializeSelectedProducer();
            InitializeSelectedGroups();
        }

        protected void EndInitializeCommon(IEnumerable<Product> entities)
        {
            InitializePageSizeFilter();
            InitializeSortFilter();
            InitializeProducerFilter();
            InitializeGroupFilter();

            InitializePager(entities);
            InitializeSelectedProducer();
            InitializeSelectedGroups();
        }

        private void InitializeSort()
        {
            ViewModel.Sort.FirstOrDefault(p => p.Value == ViewModel.SortFilter.Sort).IsSelected = true;
        }

        private void InitializePageSize()
        {
            ViewModel.PageSize.FirstOrDefault(p => p.Value == ViewModel.PageSizeFilter.PageSize).IsSelected = true;
        }

        private void InitializeProducerFilter(IQueryable<Product> entities)
        {
            if (ViewModel.ProducersFilter == null)
            {
                return;
            }

            List<GenericFilterModel<string>> producers =
                entities.GroupBy(p => p.Producer)
                        .Select(
                            g =>
                            new GenericFilterModel<string>
                                {
                                    Value = g.Key.Name,
                                    Name = g.Key.DisplayName,
                                    ProductsCount = g.Count()
                                })
                        .ToList();
            ViewModel.ProducersFilter.ProducersList =
                producers.Select(obj => obj.Value.ToLower())
                         .Where(p => ViewModel.ProducersFilter.ParsedProducers.Contains(p.ToLower()))
                         .ToList();
            foreach (var item in producers)
            {
                item.IsSelected = ViewModel.ProducersFilter.ProducersList.Contains(item.Value.ToLower());
            }

            ViewModel.Producers = producers;
        }


        protected void StartInitializeCommon(int count)
        {
            ViewModel.Count = count;
            InitializeSort();
            InitializePageSize();
        }

        public IQueryable<Product> StartInitialize(IQueryable<Product> entities)
        {
            StartInitializeCommon(entities.Count());
            InitializeProducerFilter(entities);
            InitializeTypesFilter(entities);

            if (ViewModel.GroupsFilter != null && ViewModel.GroupsFilter.GroupList.Any())
            {
                entities = entities.Where(o => ViewModel.GroupsFilter.GroupList.Contains(o.GroupName));
            }

            if (ViewModel.ProducersFilter != null && ViewModel.ProducersFilter.ProducersList.Any())
            {
                entities = entities.Where(o => ViewModel.ProducersFilter.ProducersList.Contains(o.Producer.Name));
            }

            return entities;
        }

        private void InitializeTypesFilter(IQueryable<Product> entities)
        {
            if (ViewModel.GroupsFilter == null)
            {
                return;
            }

            IEnumerable<Group> allGroups = GroupRepository.All();

            List<GenericFilterModel<string>> groups =
                entities.GroupBy(item => item.GroupName)
                        .ToList()
                        .Select(
                            g =>
                            new GenericFilterModel<string>
                                {
                                    Value = g.Key,
                                    Name = allGroups.Single(obj => obj.Name == g.Key).DisplayName,
                                    ProductsCount = g.Count()
                                })
                        .ToList();
            foreach (var item in groups)
            {
                item.IsSelected = ViewModel.GroupsFilter.GroupList.Contains(item.Value.ToLower());
            }

            ViewModel.Groups = groups;
        }
    }
}