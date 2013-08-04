namespace WebMarket.DAL.Entities
{
    public class Converter: Regulator
    {
        public Converter()
        {
            this.Types.Add("Звичайні");
        }

        public double MaxChargingCurrent { get; set; }
    }
}
