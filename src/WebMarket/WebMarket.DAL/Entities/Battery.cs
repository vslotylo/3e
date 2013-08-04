using WebMarket.DAL.Infrustructure;

namespace WebMarket.DAL.Entities
{
    public class Battery : Product
    {
        public Battery()
        {
            this.Types.Add("Кислотно-лужні");
            this.Types.Add("Гелеві");        
        }

        public double Voltage { get; set; }
        public double Volume { get; set; }

        protected override void InitializeProductInfos()
        {
            base.InitializeProductInfos();
            if(Voltage > 0)
            {
                this.Infos.Add(new ProductInfo { Name = "Напруга", Value = string.Format("{0} {1}", this.Voltage, "В")});
            }
            if (Volume > 0)
            {
                this.Infos.Add(new ProductInfo { Name = "Ємність", Value = string.Format("{0} {1}", this.Volume, "A/г") });
            }            
        }
    }
}
