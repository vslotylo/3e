namespace WebMarket.Repository.Entities
{
    public class OrderItem
    {
        public int Id { get; private set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public double TotalItemPrice
        {
            get
            {
                return Quantity * UnitPrice;
            }
        }
        public double UnitPrice { get; set; }
    }
}
