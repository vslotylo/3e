using System.ComponentModel.DataAnnotations;

namespace WebMarket.Repository.Entities
{
    public class Group
    {
        [Key]
        public string Name { get; set; }
        public string DisplayName { get; set; }
    }
}
