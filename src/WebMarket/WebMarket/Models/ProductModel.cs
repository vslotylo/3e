using System.Web.Mvc;
using WebMarket.Repository.Entities;

namespace WebMarket.Models
{
    public class ProductModel : ModelBase<Product>
    {
        public ProductModel()
        {
        }

        public ProductModel(Product product)
        {
            Name = product.Name;
            Discount = product.Discount;
            Price = product.Price;
            Description = product.Description;
            DisplayName = product.DisplayName;
            CategoryName = product.CategoryName;
            Id = product.Id;
            Producer = product.Producer;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public string DisplayName { get; set; }
        public double Discount { get; set; }
        public double Price { get; set; }

        [AllowHtml]
        public string Description { get; set; }

        public Producer Producer { get; set; }

        public override Product ToEntity(Product original)
        {
            original.DisplayName = DisplayName.Trim();
            original.Discount = Discount;
            original.Price = Price;
            original.Description = Description == null ? string.Empty : Description.Trim();
            //original.DisplayName = this.DisplayName.Trim();
            //original.CategoryName = this.CategoryName.Trim();
            original.Id = Id;
            //original.Producer = this.Producer;
            return original;
        }
    }
}