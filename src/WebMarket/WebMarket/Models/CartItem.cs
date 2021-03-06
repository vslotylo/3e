﻿using WebMarket.Repository.Entities;

namespace WebMarket.Models
{
    public class CartItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }

        public double TotalItemPrice
        {
            get { return Quantity*Product.CalculatedPrice.PriceFinalUah; }
        }
    }
}