using System;
using System.Collections.Generic;
using System.Linq;
using WebMarket.Repository.Entities.Enums;

namespace WebMarket.Repository.Entities
{
    public class Order
    {
        public Order()
        {
            Items = new List<OrderItem>();
        }

        public int Id { get; set; }
        public string User { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public DateTime CreationDate { get; set; }
        public string Comment { get; set; }
        public Status Status { get; set; }
        public List<OrderItem> Items { get; private set; }
        public DateTime? CloseDate { get; set; }

        public double Total
        {
            get
            {
                return this.Items.Sum(item => item.TotalItemPrice);
            }
        }

        
    }
}
