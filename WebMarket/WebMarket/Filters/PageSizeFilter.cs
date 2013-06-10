using WebMarket.Common;

namespace WebMarket.Filters
{
    public class PageSizeFilter : FilterBase
    {
        public PageSizeFilter()
        {
            this.PageSize = Constants.PageSizeDefault;
            this.DefaultValue = this.PageSize.ToString();
            this.Key = "pagesize";            
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