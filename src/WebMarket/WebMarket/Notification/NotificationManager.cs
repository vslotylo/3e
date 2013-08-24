namespace WebMarket.Notification
{
    public class NotificationManager
    {
        private static readonly NotificationManager Manager = new NotificationManager();

        private NotificationManager() {}

        public static NotificationManager Current
        {
            get
            {
                return Manager;
            }
        }

        public void Notify(NotificationMessage message)
        {
            EmailManager.Current.SendEmail(message);
        }
    }
}