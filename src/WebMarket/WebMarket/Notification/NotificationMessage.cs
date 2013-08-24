using System.Collections.Generic;
using WebMarket.Notification.Templates;

namespace WebMarket.Notification
{
    public class NotificationMessage
    {
        public IEnumerable<string> To { get; set; }
        public EmailTemplate EmailTemplate { get; set; }
    }
}