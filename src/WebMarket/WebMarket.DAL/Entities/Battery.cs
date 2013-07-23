using WebMarket.DAL.Infrustructure;

namespace WebMarket.DAL.Entities
{
    public class Battery : Product
    {
        public Battery()
        {
            types.Add("Кислотно-лужні");
            types.Add("Гелеві");        
        }

        public double Voltage { get; set; }
        public double Volume { get; set; }

        protected override void InitializeProductInfos()
        {
            base.InitializeProductInfos();
            if(Voltage > 0)
            {
                infos.Add(new ProductInfo { Name = "Напруга", Value = string.Format("{0} {1}", this.Voltage, "В")});
            }
            if (Volume > 0)
            {
                infos.Add(new ProductInfo { Name = "Ємність", Value = string.Format("{0} {1}", this.Volume, "A/г") });
            }            
        }
    }
}
