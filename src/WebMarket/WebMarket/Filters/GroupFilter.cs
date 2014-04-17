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
            Group = string.Empty;
            DefaultValue = Group;
            Key = "gr";
            Routes = new List<RouteValueDictionary>();
            DisplayList = new List<string>();
        }

        public string Group { get; set; }
        public IList<RouteValueDictionary> Routes { get; private set; }

        public IList<string> GroupList
        {
            get
            {
                if (groupList == null)
                {
                    groupList =
                        Group.Split(Seperators, StringSplitOptions.RemoveEmptyEntries).Select(t => t.ToLower()).ToList();
                }

                return groupList;
            }
        }

        public IList<string> DisplayList { get; private set; }

        public override string Value
        {
            get { return Group; }
        }

        public override string AddPart(string item)
        {
            List<string> list = GroupList.ToList();
            list.Add(item.ToLower());
            return string.Join(Seperators[0], list);
        }

        public override string RemovePart(string item)
        {
            List<string> list = GroupList.ToList();
            list.Remove(item.ToLower());
            return string.Join(Seperators[0], list);
        }

        public override bool Contains(string value)
        {
            return GroupList.Any(p => string.Compare(p, value, StringComparison.InvariantCultureIgnoreCase) == 0);
        }
    }
}