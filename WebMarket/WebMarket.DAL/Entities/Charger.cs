namespace WebMarket.DAL.Entities
{
    public class Charger : ElectricProduct
    {
        public Charger()
        {
            types.Add("Звичайні");
            types.Add("Потужні");        
        }

        public string OutputVoltage { get; set; }
        public double OutputCurrent { get; set; }
    }
}
