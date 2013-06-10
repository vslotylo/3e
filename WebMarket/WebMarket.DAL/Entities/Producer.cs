using WebMarket.DAL.Entities.Enums;

namespace WebMarket.DAL.Entities
{
    public class Producer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Currency BuyCurrency { get; set; }
        public double UsdRate { get; set; }
    }
}
