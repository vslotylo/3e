namespace WebMarket.Core
{
    public class Metadata
    {
        public Metadata(string displayCategory, string title, string detailTitle)
        {
            this.ListTitle = title;
            this.DetailTitle = detailTitle;
            this.DisplayCategory = displayCategory;
        }

        public string ListTitle { get; private set; }
        public string DetailTitle { get; private set; }
        public object DisplayCategory { get; private set; }
    }
}