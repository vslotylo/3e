using System.ComponentModel.DataAnnotations;

namespace WebMarket.DAL.Entities
{
    public class Metadata
    {
        [Key]
        public string CategoryName { get; set; }
        public string TitleList { get; set; }
        public string TitleDetails { get; set; }
        public string MetaListDescription { get; set; }
        public string MetadataDetailsDescription { get; set; }
    }
}