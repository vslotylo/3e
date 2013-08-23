using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;

namespace WebMarket.Filters
{
    public class ProducersFilter : FilterBase
    {
        private List<string> producersList;
        public ProducersFilter()
        {
            this.Producers = string.Empty;
            this.DefaultValue = this.Producers;
            this.Key = "producers";
            this.Routes = new List<RouteValueDictionary>();
        }

        public string Producers { get; set; }

        public IList<string> ProducersList
        {
            get
            {
                if (this.producersList == null)
                {
                    this.producersList = this.Producers.Split(this.Seperators, StringSplitOptions.RemoveEmptyEntries).Select(obj=>obj.ToLower()).ToList();
                }

                return this.producersList;
            }
        }

        public IList<RouteValueDictionary> Routes { get; set; }

        public override string Value
        {
            get
            {
                return this.Producers;
            }
        }

        public override bool IsEmpty()
        {
            if (this.ProducersList.Count == 0)
            {
                return true;
            }

            return false;
        }

        public override string AddPart(string item)
        {
            var list = this.ProducersList.ToList();
            list.Add(item.ToLower());
            return string.Join(this.Seperators[0], list);
        }

        public override string RemovePart(string item)
        {
            var list = this.ProducersList.ToList();
            list.Remove(item.ToLower());
            return string.Join(this.Seperators[0], list);
        }

        public override bool Contains(string value)
        {
            return this.ProducersList.Any(p => string.Compare(p, value, StringComparison.InvariantCultureIgnoreCase) == 0);
        }
    }
}