using System;
using System.Collections.Generic;
using System.Linq;

namespace WebMarket.Filters
{
    public class PowerCapacityFilter : FilterBase
    {
        private IList<Tuple<double,double>> powerCapacityList;
        public PowerCapacityFilter()
        {
            this.DefaultValue = string.Empty;
            this.PowerCapacity = this.DefaultValue;
            this.Key = "powercapacity";
        }

        public string PowerCapacity { get; set; }
        public IList<Tuple<double, double>> PowerCapacityList
        {
            get
            {
                if (this.powerCapacityList == null)
                {
                    this.powerCapacityList = this.PowerCapacity.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries).Select(i =>
                    {
                        var strings = i.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Take(2).ToList();
                        double min = double.Parse(strings[0].Replace("(", string.Empty).Trim());
                        double max = double.Parse(strings[1].Replace(")", string.Empty).Trim());
                        return new Tuple<double, double>(min, max);
                    }).ToList();
                }

                return this.powerCapacityList;
            }
        }
        
        public override string Value
        {
            get { return this.PowerCapacity; }
        }

        public override string AddPart(string item)
        {
            var list = this.PowerCapacityList.Select(t => t.ToString()).ToList();
            list.Add(item);
            return string.Join(this.Seperators[0], list);
        }

        public override string RemovePart(string item)
        {
            var list = this.PowerCapacityList.Select(t => t.ToString()).ToList();
            list.Remove(item);
            return string.Join(this.Seperators[0], list);
        }

        public override bool Contains(string value)
        {
            return this.PowerCapacityList.Any(p => string.Compare(p.ToString(), value, StringComparison.InvariantCultureIgnoreCase) == 0);
        }
    }
}
