using WebMarket.DAL.Entities;

namespace WebMarket.Core
{
    public class DetailsViewModel
    {
        public DetailsViewModel(Product product, Metadata metadata)
        {
            this.Product = product;
            this.Metadata = metadata;
        }

        public Product Product { get; private set; }
        public Metadata Metadata { get; private set; }
    }
}