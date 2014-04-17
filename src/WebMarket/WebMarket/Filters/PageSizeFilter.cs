using WebMarket.Common;

namespace WebMarket.Filters
{
    public class PageSizeFilter : FilterBase
    {
        public const string KeyName = "pagesize";

        public PageSizeFilter()
        {
            PageSize = Constants.PageSizeDefault;
            DefaultValue = PageSize.ToString();
            Key = KeyName;
        }

        public int PageSize { get; set; }

        public override string Value
        {
            get { return PageSize.ToString(); }
        }
    }
}