using WebMarket.DAL.Infrustructure;

namespace WebMarket.DAL.Entities
{
    public abstract class ElectricProduct : Product
    {
        public string InputRange { get; set; }

        protected override void InitializeProductInfos()
        {
            base.InitializeProductInfos();
            if (!string.IsNullOrEmpty(InputRange))
            {
                infos.Add(new ProductInfo { Name = "Напруга на вході приладу", Value = InputRange, IsPreview = false });
            }
        }
    }
}
