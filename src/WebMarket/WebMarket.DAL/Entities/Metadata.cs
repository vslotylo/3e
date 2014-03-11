using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebMarket.DAL.Entities
{
    public class Metadata
    {
        [Key]
        public string CategoryName { get; set; }
        [ForeignKey("CategoryName")]
        public Category Category { get; set; }
        public string TitleList { get; set; }
        public string TitleDetails { get; set; }
        public string MetaListDescription { get; set; }
        public string MetadataDetailsDescription { get; set; }
    }
}