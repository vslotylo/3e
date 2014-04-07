using System;

namespace WebMarket.Repository.Core
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}
