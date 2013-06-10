using System.Web.Routing;

namespace WebMarket.Core
{
    public class GenericFilterModelBase<T>
    {
        private string name;

        public string Name
        {
            get
            {
                if (string.IsNullOrEmpty(this.name))
                {
                    return this.Value.ToString();
                }

                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        public bool IsSelected { get; set; }
        public T Value { get; set; }

        public RouteValueDictionary Routes { get; set; } 
    }

    public class GenericFilterModel<T> : GenericFilterModelBase<T>
    {
        public int ProductsCount { get; set; }        
    }
}