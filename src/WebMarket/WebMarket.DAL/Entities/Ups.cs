namespace WebMarket.DAL.Entities
{
    public class Ups : Regulator
    {
        public Ups()
        {
            types.Add("Компютерні");
            types.Add("Інтерактивні");
            types.Add("On-line");            
        }

        public double BatteryVoltage {get;set;}
        public double BatteryVolume {get;set;}
        public int BatteryCount { get; set; }        
    }
}
