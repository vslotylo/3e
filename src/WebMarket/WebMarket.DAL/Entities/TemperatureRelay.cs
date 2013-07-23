namespace WebMarket.DAL.Entities
{
    public class TemperatureRelay : MesurmentUnit
    {
        public TemperatureRelay()
        {
            types.Add("Одноканальні");
            types.Add("Двоканальні");
            types.Add("Триканальні");
        }
    }
}
