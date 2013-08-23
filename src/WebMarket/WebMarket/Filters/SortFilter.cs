namespace WebMarket.Filters
{
    public class SortFilter : FilterBase
    {
        public const string KeyName = "sort";

        public SortFilter()
        {
            this.Sort = (int)Core.Sort.PriceAsc;
            this.DefaultValue = this.Sort.ToString();
            this.Key = KeyName;
        }

        public int Sort { get; private set; }
        public override string Value
        {
            get
            {
                return this.Sort.ToString();
            }
        }        
    }
}