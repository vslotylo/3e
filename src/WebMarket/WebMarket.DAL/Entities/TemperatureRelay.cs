namespace WebMarket.DAL.Entities
{
    public class TemperatureRelay : MesurmentUnit
    {
        public TemperatureRelay()
        {
            this.Types.Add("Одноканальні");
            this.Types.Add("Двоканальні");
            this.Types.Add("Триканальні");
        }
    }
}
