namespace WebMarket.Filters
{
    public class SearchFilter : FilterBase
    {
        public SearchFilter()
        {
            this.Keyword = string.Empty;
            this.DefaultValue = this.Keyword;
            this.Key = "keyword";            
        }

        public string Keyword { get; set; }
        public override string Value
        {
            get
            {
                return this.Keyword;
            }
        }            
    }
}