using System.Web.Http;
using System.Web.Mvc;
using Unity;
using Unity.Lifetime;
using Unity.WebApi;
using WebApi.Data.DataContexts;
using WebApi.Data.Repositories;
using WebApi.Domain.Contracts;

namespace WebApi.Api
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<BookStoreDataContext>(new HierarchicalLifetimeManager());
            container.RegisterType<ILivroRepository, LivroRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IAutorRepository, AutorRepository>(new HierarchicalLifetimeManager());
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}