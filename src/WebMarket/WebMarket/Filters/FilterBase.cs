using System;

namespace WebMarket.Filters
{
    public abstract class FilterBase
    {
        protected readonly string[] Seperators = new[] {"-"};
        public string Key { get; protected set; }
        public abstract string Value { get; }
        public string DefaultValue { get; set; }

        public virtual bool IsEmpty()
        {
            if (string.Compare(Value, DefaultValue, StringComparison.InvariantCultureIgnoreCase) == 0)
            {
                return true;
            }

            return false;
        }

        public virtual string RemovePart(string item)
        {
            return string.Empty;
        }

        public virtual bool Contains(string value)
        {
            return false;
        }

        public virtual string AddPart(string value)
        {
            return value;
        }
    }
}