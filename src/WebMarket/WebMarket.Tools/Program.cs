using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMarket.Tools.Products;

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
