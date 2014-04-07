using System.ComponentModel.DataAnnotations;

namespace WebMarket.Repository.Entities
{
    public class Category
    {
        [Key]
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string TitleList { get; set; }
        public string TitleDetails { get; set; }
        public string MetaListDescription { get; set; }
        public string MetadataDetailsDescription { get; set; }
    }
}
