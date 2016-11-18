using System.ServiceModel.Activation;
using System.Web.Mvc;
using System.Web.Routing;
using WeatherStationServer.api;

namespace WeatherStationServer
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.Add(new ServiceRoute("api", new WebServiceHostFactory(), typeof(RestService)));

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
