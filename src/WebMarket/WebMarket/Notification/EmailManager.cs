using System.ComponentModel;
using System.Net;
using System.Net.Mail;
using System.Text;
using WebMarket.Common;
using log4net;

namespace WebMarket.Notification
{
    public class EmailManager
    {
        private readonly ILog logger = LogManager.GetLogger(typeof(EmailManager));
        private static readonly EmailManager Manager = new EmailManager();

        private EmailManager() {}

        public static EmailManager Current
        {
            get
            {
                return Manager;
            }
        }

        public void SendEmail(NotificationMessage message)
        {
            var basicAuthenticationInfo = new NetworkCredential(Settings.SmtpUserAccount, Settings.SmtpUserPassword);
            var smtp = new SmtpClient(Settings.SmtpHost)
            {
                UseDefaultCredentials = false,
                Credentials = basicAuthenticationInfo
            };

            var mailMessage = new MailMessage();

            foreach (var toEmail in message.To)
            {
                mailMessage.To.Add(toEmail);
            }
            foreach (var replyToEmail in Settings.ReplyTo)
            {
                mailMessage.ReplyToList.Add(replyToEmail);
            }
            foreach (var ccEmail in Settings.CC)
            {
                mailMessage.CC.Add(ccEmail);
            }
            foreach (var bccEmail in Settings.BCC)
            {
                mailMessage.Bcc.Add(bccEmail);
            }

            mailMessage.Subject = message.Subject;
            mailMessage.SubjectEncoding = Encoding.UTF8;
            mailMessage.From = new MailAddress(Settings.From);
            
            mailMessage.BodyEncoding = Encoding.UTF8;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = message.Body;
            smtp.SendCompleted += this.SendCompleted;
            smtp.SendAsync(mailMessage, null);
        }

        void SendCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                this.logger.Error(e);
            }

            var client = sender as SmtpClient;
            if (client != null)
            {
                client.SendCompleted -= this.SendCompleted;
            }
        }
    }
}