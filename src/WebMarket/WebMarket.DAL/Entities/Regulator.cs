using WebMarket.DAL.Infrustructure;

namespace WebMarket.DAL.Entities
{
    public class Regulator : ElectricProduct
    {
        public double PowerCapacity { get; set; }
        public int PhaseCount { get; set; }
        protected override void InitializeProductInfos()
        {
            base.InitializeProductInfos();
            if (PowerCapacity > 0)
            {
                infos.Add(new ProductInfo { Name = "Потужність", Value = string.Format("{0} {1}", this.PowerCapacity, "кВт"), IsPreview = true });
            }
            if (PhaseCount > 0)
            {
                infos.Add(new ProductInfo { Name = "Кількість фаз", Value = string.Format("{0}", this.PhaseCount), IsPreview = true });
            }
        }
    }
}
