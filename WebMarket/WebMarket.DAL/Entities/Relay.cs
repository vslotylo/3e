namespace WebMarket.DAL.Entities
{
    public abstract class Relay : MesurmentUnit
    {
        public string UpperLimitClearance { get; set; }
        public string LowerLimitClearance { get; set; }
    }
}
