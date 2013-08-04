﻿namespace WebMarket.DAL.Entities
{
    public class Ups : Regulator
    {
        public Ups()
        {
            this.Types.Add("Компютерні");
            this.Types.Add("Інтерактивні");
            this.Types.Add("On-line");            
        }

        public double BatteryVoltage {get;set;}
        public double BatteryVolume {get;set;}
        public int BatteryCount { get; set; }        
    }
}
