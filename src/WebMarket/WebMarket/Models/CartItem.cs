using WebMarket.DAL.Entities;

namespace WebMarket.Models
{
    public class CartItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public double TotalItemPrice
        {
            get
            {
                return this.Quantity * this.Product.CalculatedPrice.PriceFinalUah;
            }
        }
    }
}
