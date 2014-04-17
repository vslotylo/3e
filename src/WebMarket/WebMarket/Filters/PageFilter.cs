namespace WebMarket.Filters
{
    public class PageFilter : FilterBase
    {
        public const string KeyName = "page";

        public PageFilter()
        {
            DefaultValue = "1";
            Page = 1;
            Key = KeyName;
        }

        public int Page { get; set; }

        public override string Value
        {
            get { return Page.ToString(); }
        }
    }
}