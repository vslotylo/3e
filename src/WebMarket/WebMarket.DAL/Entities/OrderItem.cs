namespace WebMarket.DAL.Entities
{
    public class OrderItem
    {
        public int Id { get; private set; }
        public string Pid { get; set; }
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
