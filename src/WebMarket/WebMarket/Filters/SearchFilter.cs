namespace WebMarket.Filters
{
    public class SearchFilter : FilterBase
    {
        public SearchFilter()
        {
            Keyword = string.Empty;
            DefaultValue = Keyword;
            Key = "keyword";
        }

        public string Keyword { get; set; }

        public override string Value
        {
            get { return Keyword; }
        }
    }
}