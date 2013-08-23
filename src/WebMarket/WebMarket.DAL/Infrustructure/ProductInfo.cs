namespace WebMarket.DAL.Infrustructure
{
    public class ProductInfo
    {
        public ProductInfo(string key, object value, bool isBool = false)
        {
            this.Key = key;
            this.Value = value;
            this.IsBool = isBool;
        }

        public string Key { get; set; }
        public object Value { get; set; }
        public bool IsBool { get; set; }
    }
}
