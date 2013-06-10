namespace WebMarket.DAL.Entities
{
    public class Voltmeter : MesurmentUnit
    {
        public Voltmeter()
        {
            types.Add("AC");
            types.Add("DC");
        }
    }
}
