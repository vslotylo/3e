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
            Producers = string.Empty;
            DefaultValue = Producers;
            Key = "producers";
            Routes = new List<RouteValueDictionary>();
            DisplayList = new List<string>();
        }

        public string Producers { get; set; }

        public IList<string> ParsedProducers
        {
            get
            {
                if (producersList == null)
                {
                    producersList =
                        Producers.Split(Seperators, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(o => o.ToLower())
                                 .ToList();
                }

                return producersList;
            }
        }

        public IList<string> ProducersList { get; set; }

        public IList<RouteValueDictionary> Routes { get; set; }

        public override string Value
        {
            get { return Producers; }
        }

        public IList<string> DisplayList { get; private set; }

        public override bool IsEmpty()
        {
            if (ProducersList.Count == 0)
            {
                return true;
            }

            return false;
        }

        public override string AddPart(string item)
        {
            List<string> list = ParsedProducers.ToList();
            list.Add(item.ToLower());
            return string.Join(Seperators[0], list);
        }

        public override string RemovePart(string item)
        {
            List<string> list = ParsedProducers.ToList();
            list.Remove(item.ToLower());
            return string.Join(Seperators[0], list);
        }

        public override bool Contains(string value)
        {
            return ProducersList.Any(p => string.Compare(p, value, StringComparison.InvariantCultureIgnoreCase) == 0);
        }
    }
}