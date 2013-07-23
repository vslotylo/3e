using System.ComponentModel;

namespace WebMarket.DAL.Entities.Enums
{
    public enum OrderStatus
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
