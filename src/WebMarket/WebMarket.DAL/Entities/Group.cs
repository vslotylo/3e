using System.ComponentModel.DataAnnotations;

namespace WebMarket.DAL.Entities
{
    public class Group
    {
        [Key]
        public string Name { get; set; }
        public string DisplayName { get; set; }
    }
}
