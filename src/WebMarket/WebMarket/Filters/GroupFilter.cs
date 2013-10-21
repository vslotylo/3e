using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;

namespace WebMarket.Filters
{
    public class GroupFilter : FilterBase
    {
        private IList<string> groupList;
        public GroupFilter()
        {
            this.Group = string.Empty;
            this.DefaultValue = this.Group;
            this.Key = "gr";
            this.Routes = new List<RouteValueDictionary>();
            this.DisplayList = new List<string>();
        }

        public string Group { get; set; }
        public IList<RouteValueDictionary> Routes { get; private set; }
        public IList<string> GroupList
        {
            get
            {
                if (this.groupList == null)
                {
                    this.groupList = this.Group.Split(this.Seperators, StringSplitOptions.RemoveEmptyEntries).Select(t => t.ToLower()).ToList();
                }

                return this.groupList;
            }
        }

        public IList<string> DisplayList
        {
            get; private set;
        }

        public override string Value
        {
            get { return this.Group; }
        }

        public override string AddPart(string item)
        {
            var list = this.GroupList.ToList();
            list.Add(item.ToLower());
            return string.Join(this.Seperators[0], list);
        }

        public override string RemovePart(string item)
        {
            var list = this.GroupList.ToList();
            list.Remove(item.ToLower());
            return string.Join(this.Seperators[0], list);
        }

        public override bool Contains(string value)
        {
            return this.GroupList.Any(p => string.Compare(p, value, StringComparison.InvariantCultureIgnoreCase) == 0);
        }
    }
}
