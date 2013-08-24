using System.Collections.Generic;

namespace WebMarket.Notification
{
    public class NotificationMessage
    {
        public IEnumerable<string> To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}