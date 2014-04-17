using System.Collections.Generic;
using System.Linq;

namespace WebMarket.Models
{
    public class Cart
    {
        public Cart()
        {
            Items = new List<CartItem>();
        }

        public IList<CartItem> Items { get; private set; }

        public double TotalPrice
        {
            get
            {
                if (Items.Any())
                {
                    return Items.Select(i => i.TotalItemPrice).Aggregate((i, j) => i + j);
                }

                return default(double);
            }
        }

        public int? TotalItems
        {
            get
            {
                if (!Items.Any())
                {
                    return null;
                }

                return Items.Select(obj => obj.Quantity).Aggregate((i, j) => i + j);
            }
        }
    }
}