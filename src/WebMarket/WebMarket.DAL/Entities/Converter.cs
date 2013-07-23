namespace WebMarket.DAL.Entities
{
    public class Converter: Regulator
    {
        public Converter()
        {
            types.Add("Звичайні");
        }

        public double MaxChargingCurrent { get; set; }
    }
}
