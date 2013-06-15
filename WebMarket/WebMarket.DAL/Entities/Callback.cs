using System;

namespace WebMarket.DAL.Entities
{
    public class Callback
    {
        public int Id { get; set; }
        public string Phone { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
