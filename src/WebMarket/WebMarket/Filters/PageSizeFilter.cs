using WebMarket.Common;

namespace WebMarket.Filters
{
    public class PageSizeFilter : FilterBase
    {
        public const string KeyName = "pagesize";

        public PageSizeFilter()
        {
            this.PageSize = Constants.PageSizeDefault;
            this.DefaultValue = this.PageSize.ToString();
            this.Key = KeyName;            
        }

        public int PageSize { get; set; }
        public override string Value
        {
            get
            {
                return this.PageSize.ToString();
            }
        }              
    }
}