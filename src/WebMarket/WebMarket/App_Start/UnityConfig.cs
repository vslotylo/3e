using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using WebMarket.Repository;
using WebMarket.Repository.Core;
using WebMarket.Repository.Current;
using WebMarket.Repository.Data.Import;
using WebMarket.Repository.Interfaces;

namespace WebMarket.App_Start
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            SetupUnitOfWork(container);
            SetupRepositories(container);
            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        private static void SetupUnitOfWork(UnityContainer container)
        {
            container.RegisterType<IDbContextFactory<WebMarketDbContext>, DbContextFactory>();
            container.RegisterType<IUnitOfWork, UnitOfWork<WebMarketDbContext>>(new PerResolveLifetimeManager());
        }

        private static void SetupRepositories(UnityContainer container)
        {
            container.RegisterType<IProductRepository, ProductRepository>();
            container.RegisterType<ICallbackRepository, CallbackRepository>();
            container.RegisterType<ICategoryRepository, CategoryRepository>();
            container.RegisterType<IGroupRepository, GroupRepository>();
            container.RegisterType<IOrderRepository, OrderRepository>();
            container.RegisterType<IProducerRepository, ProducerRepository>();
            container.RegisterType<IProductRepository, ProductRepository>();
            container.RegisterType<IDataImporter, DataImporter>();
        }
    }
}