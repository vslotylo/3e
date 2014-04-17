using System;
using System.Collections.Generic;
using System.Linq;

namespace WebMarket.Filters
{
    public class PowerCapacityFilter : FilterBase
    {
        private IList<Tuple<double, double>> powerCapacityList;

        public PowerCapacityFilter()
        {
            DefaultValue = string.Empty;
            PowerCapacity = DefaultValue;
            Key = "powercapacity";
        }

        public string PowerCapacity { get; set; }

        public IList<Tuple<double, double>> PowerCapacityList
        {
            get
            {
                if (powerCapacityList == null)
                {
                    powerCapacityList =
                        PowerCapacity.Split(new[] {";"}, StringSplitOptions.RemoveEmptyEntries).Select(i =>
                            {
                                List<string> strings =
                                    i.Split(new[] {","}, StringSplitOptions.RemoveEmptyEntries).Take(2).ToList();
                                double min = double.Parse(strings[0].Replace("(", string.Empty).Trim());
                                double max = double.Parse(strings[1].Replace(")", string.Empty).Trim());
                                return new Tuple<double, double>(min, max);
                            }).ToList();
                }

                return powerCapacityList;
            }
        }

        public override string Value
        {
            get { return PowerCapacity; }
        }

        public override string AddPart(string item)
        {
            List<string> list = PowerCapacityList.Select(t => t.ToString()).ToList();
            list.Add(item);
            return string.Join(Seperators[0], list);
        }

        public override string RemovePart(string item)
        {
            List<string> list = PowerCapacityList.Select(t => t.ToString()).ToList();
            list.Remove(item);
            return string.Join(Seperators[0], list);
        }

        public override bool Contains(string value)
        {
            return
                PowerCapacityList.Any(
                    p => string.Compare(p.ToString(), value, StringComparison.InvariantCultureIgnoreCase) == 0);
        }
    }
}