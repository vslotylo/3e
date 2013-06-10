using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;
using WebMarket.Filters;

namespace WebMarket.Core
{
    public class FilterList : List<FilterBase>
    {
        private readonly string[] staticFilters = new string[] { "page", "pagesize", "sort" };

        private RouteValueDictionary routes;

        public bool IsEmpty()
        {
            bool isEmpty = true;
            base.ForEach((filter) =>
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
                    base.ForEach(filter =>
                    {
                        this.routes.Add(filter.Key, filter.Value);
                    });
                }

                return this.routes;
            }
        }

        public RouteValueDictionary UpdateFilter(FilterBase filter, object value)
        {
            string strValue = value.ToString();
            var routes = new RouteValueDictionary();
            foreach (var route in this.Routes)
            {
                routes[route.Key] = route.Value;
            }

            routes[PageFilter.KeyName] = PageFilter.DefaultValueStatic;

            bool contains = filter.Contains(strValue);
            if (!contains)
            {
                routes[filter.Key] = filter.AddPart(strValue);
            }
            else
            {
                routes[filter.Key] = filter.RemovePart(strValue);
            }

            return routes;
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
                dictionary.Add(route.Key, route.Value);
            }

            dictionary[PageFilter.KeyName] = PageFilter.DefaultValueStatic;

            dictionary[filter.Key] = value;

            return dictionary;
        }        
    }
}
