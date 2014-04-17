namespace WebMarket.Filters
{
    public class CategoryFilter : FilterBase
    {
        public CategoryFilter()
        {
            Category = string.Empty;
            DefaultValue = Category;
            Key = "category";
        }

        public string Category { get; set; }

        public override string Value
        {
            get { return Category; }
        }
    }
}