namespace WebMarket.Extensions
{
    public static class FormatExtensions
    {
        public static string ToFormatString(this double obj)
        {
            // TODO
            return obj.ToString("### ### ###.##");
        }
    }
}