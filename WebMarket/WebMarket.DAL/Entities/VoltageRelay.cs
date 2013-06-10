using WebMarket.DAL.Infrustructure;

namespace WebMarket.DAL.Entities
{
    public class VoltageRelay : Relay
    {
        public VoltageRelay()
        {
            types.Add("Побутові однофазні");
            types.Add("Побутові трифазні");
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
                infos.Add(new ProductInfo { Name = "Номіналий струм", Value = string.Format("{0}A", NominalCurrent), IsPreview = true });
            }
            if (FullLoadPowerCapacity > 0)
            {
                infos.Add(new ProductInfo { Name = "Максимальний струм на контактах", Value = string.Format("{0}A", FullLoadCurrent), IsPreview = true });
            }
            if (FullLoadCurrent > 0)
            {
                infos.Add(new ProductInfo { Name = "Максимальний напруга на контактах", Value = string.Format("{0}кВт", FullLoadPowerCapacity), IsPreview = true });
            }
            if (!string.IsNullOrEmpty(UpperLimitClearance))
            {
                infos.Add(new ProductInfo { Name = "Час відключення по верхній межі", Value = UpperLimitClearance, IsPreview = true });
            }
            if (!string.IsNullOrEmpty(LowerLimitClearance))
            {
                infos.Add(new ProductInfo { Name = "Час відключення по нижній межі", Value = LowerLimitClearance, IsPreview = true });
            }
            if (!string.IsNullOrEmpty(DisplayVoltageRange))
            {
                infos.Add(new ProductInfo { Name = "Напруга, що вимірються", Value = DisplayVoltageRange, IsPreview = false });
            }
        }
    }
}
