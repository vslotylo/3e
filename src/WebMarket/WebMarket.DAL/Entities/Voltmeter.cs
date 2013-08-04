namespace WebMarket.DAL.Entities
{
    public class Voltmeter : MesurmentUnit
    {
        public Voltmeter()
        {
            this.Types.Add("AC");
            this.Types.Add("DC");
        }
    }
}
