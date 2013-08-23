﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;

namespace WebMarket.Filters
{
    public class TypeFilter : FilterBase
    {
        private IList<string> typeList;
        public TypeFilter()
        {
            this.Type = string.Empty;
            this.DefaultValue = this.Type;
            this.Key = "type";
            this.Routes = new List<RouteValueDictionary>();
            this.DisplayList = new List<string>();
        }

        public string Type { get; set; }
        public IList<RouteValueDictionary> Routes { get; private set; }
        public IList<string> TypeList
        {
            get
            {
                if (this.typeList == null)
                {
                    this.typeList = this.Type.Split(this.Seperators, StringSplitOptions.RemoveEmptyEntries).Select(t => t.ToLower()).ToList();
                }

                return this.typeList;
            }
        }

        public IList<string> DisplayList
        {
            get; private set;
        }

        public override string Value
        {
            get { return this.Type; }
        }

        public override string AddPart(string item)
        {
            var list = this.TypeList.ToList();
            list.Add(item.ToLower());
            return string.Join(this.Seperators[0], list);
        }

        public override string RemovePart(string item)
        {
            var list = this.TypeList.ToList();
            list.Remove(item.ToLower());
            return string.Join(this.Seperators[0], list);
        }

        public override bool Contains(string value)
        {
            return this.TypeList.Any(p => string.Compare(p, value, StringComparison.InvariantCultureIgnoreCase) == 0);
        }
    }
}
