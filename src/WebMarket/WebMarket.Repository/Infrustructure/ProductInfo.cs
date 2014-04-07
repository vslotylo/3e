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
            this.Key = key;
            this.Value = value;
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
            get
            {
                if(this.value is bool)
                {
                    return value.ToString().Trim();
                }

                return value;
            }
            set
            {
                if (IsBool)
                {
                    this.value = bool.Parse(value.ToString());
                }
                else
                {
                    if (value is string[])
                    {
                        this.value = string.Join(",", value as string[]);
                    }
                    else
                    {
                        this.value = value.ToString().Trim();    
                    }
                    
                }
            }
        }

        public bool IsBool
        {
            get { return this.Value is bool; }
        }
    }
}
