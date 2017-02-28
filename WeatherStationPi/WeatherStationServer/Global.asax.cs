using System;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Repository;
using Repository.Interfaces;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using SimpleInjector.Integration.WebApi;

namespace WeatherStationServer
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            RegisterIocContainer();

            GlobalConfiguration.Configure(WebApiConfig.Register);

            RouteConfig.RegisterRoutes(RouteTable.Routes);

            Bundles.RegisterBundles(BundleTable.Bundles);

            //AreaRegistration.RegisterAllAreas();
        }

        private static void RegisterIocContainer()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            RegisterTypes(container);

            container.Verify();

            // MVC
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));

            // WebAPi
            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }

        private static void RegisterTypes(Container container)
        {
            container.Register<IDataPointRepository, DataPointRepository>(Lifestyle.Singleton);
            container.Register<IConnectionStringFactory, DefaultConnectionStringFactory>(Lifestyle.Singleton);
        }
    }
}