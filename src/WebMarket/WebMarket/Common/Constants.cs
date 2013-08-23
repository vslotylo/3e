namespace WebMarket.Common
{
    public static class Constants
    {
        public const int PageSizeDefault = 16;
        public const int PageSizeSmall = 32;
        public const int PageSizeBig = 64;

        public const string PriceAsc = "Від дешевшого до дорощого";
        public const string PriceDesc = "Від дорощого до дешевшого";
        public const string RateDesc = "По рейтингу";

        public const string CartKey = "Cart";
        public const string AdminRoleName = "Administrator";
    }

    public static class CategoryNames
    {
        public const string Avr = "avr"; //Стабілізатори напруги
        public const string Ups = "ups"; //ДБЖ
        public const string Battery = "battery"; //Акумулятори
        public const string Charger = "charger"; //Зарядні пристрої
        public const string Converter = "converter"; //Перетворювачі напруги
        public const string VoltageRelay = "voltage-relay"; //Реле напруги
        public const string TimeRelay = "time-relay"; //Реле часу
        public const string CurrentRelay = "current-relay"; //Реле струму
        public const string TemperatureRelay = "temperature-controller"; //Реле температури
        public const string Voltmeter = "voltmeter"; //Вольтметри
    }
}