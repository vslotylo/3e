namespace WebMarket.Filters
{
    public class SortFilter : FilterBase
    {
        public SortFilter()
        {
            this.Sort = (int)Core.Sort.PriceAsc;
            this.DefaultValue = this.Sort.ToString();
            this.Key = "sort";
        }

        public int Sort { get; set; }
        public override string Value
        {
            get
            {
                return this.Sort.ToString();
            }
        }        
    }
}