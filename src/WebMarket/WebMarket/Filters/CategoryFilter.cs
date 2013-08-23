namespace WebMarket.Filters
{
    public class CategoryFilter : FilterBase
    {
        public CategoryFilter()
        {
            this.Category = string.Empty;
            this.DefaultValue = this.Category;
            this.Key = "category";            
        }

        public string Category { get; set; }
        public override string Value
        {
            get
            {
                return this.Category;
            }
        }      
    }
}