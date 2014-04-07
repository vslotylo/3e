using System;
using WebMarket.Repository.Entities.Enums;

namespace WebMarket.Repository.Entities
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
