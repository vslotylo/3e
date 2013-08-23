namespace WebMarket.Filters
{
    public class PageFilter : FilterBase
    {
        public const string KeyName = "page";

        public PageFilter()
        {
            this.DefaultValue = "1";
            this.Page = 1;
            this.Key = KeyName;            
        }

        public int Page { get; set; }
       
        public override string Value
        {
            get
            {
                return this.Page.ToString();
            }
        }             
    }
}