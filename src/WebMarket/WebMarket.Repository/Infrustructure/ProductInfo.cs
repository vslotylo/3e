namespace WebMarket.Repository.Infrustructure
{
    public class ProductInfo
    {
        private string key = string.Empty;
        private object value = string.Empty;

        public ProductInfo()
        {
        }

        public ProductInfo(string key, object value)
        {
            Key = key;
            bool isBool;
            bool.TryParse(value.ToString(), out isBool);
            IsBool = isBool;
            Value = value;
        }

        public string Key
        {
            get { return key; }
            set
            {
                if (value != null)
                {
                    key = value.Trim();
                }
            }
        }

        public object Value
        {
            get { return value; }
            set
            {
                if (IsBool)
                {
                    this.value = bool.Parse(value.ToString());
                }
                else
                {
                    this.value = value.ToString().Trim();
                }
            }
        }

        public bool IsBool { get; private set; }
    }
}