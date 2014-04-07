using WebMarket.Repository.Entities.Enums;

namespace WebMarket.Repository.Entities
{
    public class Producer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string HomePage { get; set; }
        public string Description { get; set; }
        public Currency BuyCurrency { get; set; }
        public double UsdRate { get; set; }
    }
}
