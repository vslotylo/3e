using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;
using WebMarket.Filters;

namespace WebMarket.Core
{
    public class FilterList : List<FilterBase>
    {
        private readonly string[] staticFilters = new[] { PageFilter.KeyName, PageSizeFilter.KeyName, SortFilter.KeyName };

        private RouteValueDictionary routes;

        public bool IsEmpty()
        {
            bool isEmpty = true;
            this.ForEach((filter) =>
            {
                if (!this.staticFilters.Contains(filter.Key) && !filter.IsEmpty())
                {
                    isEmpty = false;
                }
            });

            return isEmpty;
        }

        public RouteValueDictionary Routes
        {
            get
            {
                if (this.routes == null)
                {
                    this.routes = new RouteValueDictionary();
                    this.ForEach(filter => this.routes.Add(filter.Key, filter.Value));
                }

                return this.routes;
            }
        }

        public RouteValueDictionary UpdateFilter(FilterBase filter, object value)
        {
            string strValue = value.ToString();
            var updateFilter = new RouteValueDictionary();
            foreach (var route in this.Routes)
            {
                var f = this.GetFilter(route.Key);
                if (string.Compare(route.Value.ToString(), f.DefaultValue, StringComparison.InvariantCultureIgnoreCase) != 0)
                {
                    updateFilter[route.Key] = route.Value;
                }
            }

            // routes[PageFilter.KeyName] = PageFilter.DefaultValueStatic;

            bool contains = filter.Contains(strValue);
            if (!contains)
            {
                updateFilter[filter.Key] = filter.AddPart(strValue);
            }
            else
            {
                updateFilter[filter.Key] = filter.RemovePart(strValue);
            }

            return updateFilter;
        }

        public FilterBase GetFilter(string key)
        {
            var filter = this.SingleOrDefault(f => string.Compare(f.Key, key, StringComparison.InvariantCultureIgnoreCase) == 0);
            if (filter == null)
            {
                throw new Exception(string.Format("No filter with key {0}", key));
            }

            return filter;
        }

        public RouteValueDictionary RemoveFilterPart(FilterBase filter, string item)
        {
            var value = filter.RemovePart(item);

            var dictionary = new RouteValueDictionary();
            foreach (var route in this.Routes)
            {
                //skip page for navigating to default page when removing filter
                if (route.Key == PageFilter.KeyName)
                {
                    continue;
                }

                var f = this.GetFilter(route.Key);
                if (string.Compare(route.Value.ToString(), f.DefaultValue, StringComparison.InvariantCultureIgnoreCase) != 0)
                {
                    dictionary.Add(route.Key, route.Value);
                }
            }

            dictionary[filter.Key] = value;

            return dictionary;
        }
    }
}
