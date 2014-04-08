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
            this.Name = product.Name;
            this.Discount = product.Discount;
            this.Price = product.Price;
            this.Description = product.Description;
            this.DisplayName = product.DisplayName;
            this.CategoryName = product.CategoryName;
            this.Id = product.Id;
            this.Producer = product.Producer;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public string DisplayName { get; set; }
        public double Discount { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public Producer Producer { get; set; }

        public override Product ToEntity(Product original)
        {
            original.Name = this.Name.Trim();
            original.Discount = this.Discount;
            original.Price = this.Price;
            original.Description = this.Description == null ? string.Empty : this.Description.Trim();
            //original.DisplayName = this.DisplayName.Trim();
            //original.CategoryName = this.CategoryName.Trim();
            original.Id = this.Id;
            //original.Producer = this.Producer;
            return original;
        }
    }
}