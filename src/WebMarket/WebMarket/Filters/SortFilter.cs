namespace WebMarket.Filters
{
    public class SortFilter : FilterBase
    {
        public const string KeyName = "sort";

        public SortFilter()
        {
            Sort = (int) Core.Sort.PriceAsc;
            DefaultValue = Sort.ToString();
            Key = KeyName;
        }

        public int Sort { get; set; }

        public override string Value
        {
            get { return Sort.ToString(); }
        }
    }
}