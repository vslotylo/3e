namespace WebMarket.DAL.Entities
{
    public class Charger : ElectricProduct
    {
        public Charger()
        {
            this.Types.Add("Звичайні");
            this.Types.Add("Потужні");        
        }

        public string OutputVoltage { get; set; }
        public double OutputCurrent { get; set; }
    }
}
