﻿using WebMarket.Tools.Products;

namespace WebMarket.Tools
{
    class Program
    {
        static void Main(string[] args)
        {
            var productNameProcessor = new ProductNameProcessor(@"D:\Projects\3e\src\WebMarket\WebMarket\App_Data\Products.xlsx");
            productNameProcessor.Process();
        }
    }
}
