namespace WebMarket.Notification.Templates
{
    public class EmailTemplate
    {
        public EmailTemplate(string subject, string body)
        {
            this.Subject = subject;
            this.Body = body;
        }

        public string Subject { get; set; }
        public string Body { get; set; }
    }
}