using System.ComponentModel;

namespace WebMarket.Repository.Entities.Enums
{
    public enum Status
    {
        [Description("Новий")]
        Pending,
        [Description("Опрацьовується")]
        Processing,
        [Description("Призупинений")]
        OnHold,
        [Description("Відшкодований")]
        Refunded,
        [Description("Відмінений")]
        Cancelled,
        [Description("Завершений")]
        Completed
    }
}
