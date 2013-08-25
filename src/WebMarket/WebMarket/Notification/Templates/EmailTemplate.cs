namespace WebMarket.Notification.Templates
{
    public class EmailTemplate
    {
        public EmailTemplate(string body, string subject)
        {
            this.Body = body;
            this.Subject = subject;
        }

        public string Subject { get; set; }
        public string Body { get; set; }
    }
}