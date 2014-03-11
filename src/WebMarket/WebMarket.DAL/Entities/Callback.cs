using System;
using WebMarket.DAL.Entities.Enums;

namespace WebMarket.DAL.Entities
{
    public class Callback
    {
        public int Id { get; set; }
        public string Phone { get; set; }
        public DateTime CreateTime { get; set; }
        public Status Status { get; set; }
        public string Url { get; set; }
    }
}
