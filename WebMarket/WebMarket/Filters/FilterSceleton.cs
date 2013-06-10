using System;

namespace WebMarket.Filters
{
    public abstract class FilterBase
    {
        public readonly string[] Seperators = new string[] { ";" };
        public string Key { get; protected set; }
        public abstract string Value { get; }
        public string DefaultValue { get; protected set; }

        public virtual bool IsEmpty()
        {
            if (string.Compare(this.Value, this.DefaultValue, StringComparison.InvariantCultureIgnoreCase) == 0)
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