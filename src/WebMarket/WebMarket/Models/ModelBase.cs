namespace WebMarket.Models
{
    public abstract class ModelBase<T>
    {
        public abstract T ToEntity(T original);
    }
}