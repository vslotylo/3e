using System.Collections.Generic;
using System.Linq;

namespace WebMarket.Models
{
    public class Cart
    {
        public Cart()
        {
            this.Items = new List<CartItem>();
        }

        public IList<CartItem> Items { get; private set; }

        public double TotalPrice
        {
            get
            {
                if (this.Items.Any())
                {
                    return this.Items.Select(i => i.TotalItemPrice).Aggregate((i, j) => i + j);
                }

                return default(double);
            }
        }

        public int? TotalItems
        {
            get
            {
                if (!this.Items.Any())
                {
                    return null;
                }

                return this.Items.Select(obj=>obj.Quantity).Aggregate((i, j) => i + j);
            }
        }
    }
}
