using WebMarket.DAL.Infrustructure;

namespace WebMarket.DAL.Entities
{
    public class CurrentRelay : Relay
    {
        public CurrentRelay()
        {
            this.Types.Add("Побутові однофазні");
        }

        protected override void InitializeProductInfos()
        {
            base.InitializeProductInfos();            
            
            if (!string.IsNullOrEmpty(UpperLimitClearance))
            {
                this.Infos.Add(new ProductInfo { Name = "Час відключення по верхній межі", Value = UpperLimitClearance, IsPreview = true });
            }
            if (!string.IsNullOrEmpty(LowerLimitClearance))
            {
                this.Infos.Add(new ProductInfo { Name = "Час відключення по нижній межі", Value = LowerLimitClearance, IsPreview = true });
            }
        }
    }
}
