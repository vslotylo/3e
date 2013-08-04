using WebMarket.DAL.Infrustructure;

namespace WebMarket.DAL.Entities
{
    public class VoltageRelay : Relay
    {
        public VoltageRelay()
        {
            this.Types.Add("Побутові однофазні");
            this.Types.Add("Побутові трифазні");
        }

        public double FullLoadCurrent { get; set; }
        public double FullLoadPowerCapacity { get; set; }
        public double NominalCurrent { get; set; }
        public string DisplayVoltageRange { get; set; }

        protected override void InitializeProductInfos()
        {
            base.InitializeProductInfos();
            if (NominalCurrent > 0)
            {
                this.Infos.Add(new ProductInfo { Name = "Номіналий струм", Value = string.Format("{0}A", NominalCurrent), IsPreview = true });
            }
            if (FullLoadPowerCapacity > 0)
            {
                this.Infos.Add(new ProductInfo { Name = "Максимальний струм на контактах", Value = string.Format("{0}A", FullLoadCurrent), IsPreview = true });
            }
            if (FullLoadCurrent > 0)
            {
                this.Infos.Add(new ProductInfo { Name = "Максимальний напруга на контактах", Value = string.Format("{0}кВт", FullLoadPowerCapacity), IsPreview = true });
            }
            if (!string.IsNullOrEmpty(UpperLimitClearance))
            {
                this.Infos.Add(new ProductInfo { Name = "Час відключення по верхній межі", Value = UpperLimitClearance, IsPreview = true });
            }
            if (!string.IsNullOrEmpty(LowerLimitClearance))
            {
                this.Infos.Add(new ProductInfo { Name = "Час відключення по нижній межі", Value = LowerLimitClearance, IsPreview = true });
            }
            if (!string.IsNullOrEmpty(DisplayVoltageRange))
            {
                this.Infos.Add(new ProductInfo { Name = "Напруга, що вимірються", Value = DisplayVoltageRange, IsPreview = false });
            }
        }
    }
}
