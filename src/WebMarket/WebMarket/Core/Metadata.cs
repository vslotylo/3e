namespace WebMarket.Core
{
    public class Metadata
    {
        public Metadata(string displayCategory, string title, string detailTitle, string metadataDescription, string metadataDetails)
        {
            this.DisplayCategory = displayCategory;
            this.TitleList = title;
            this.TitleDetails = detailTitle;
            this.MetaListDescription = metadataDescription;
            this.MetadataDetailsDescription = metadataDetails;
        }

        public string TitleList { get; private set; }
        public string TitleDetails { get; private set; }
        public object DisplayCategory { get; private set; }
        public string MetaListDescription { get; private set; }
        public string MetadataDetailsDescription { get; private set; }
    }
}