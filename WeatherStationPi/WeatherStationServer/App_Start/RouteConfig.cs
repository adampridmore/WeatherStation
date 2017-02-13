using System;
using System.ServiceModel.Activation;
using System.Web.Mvc;
using System.Web.Routing;
using WeatherStationServer.api;
using WeatherStationServer.Controllers;

namespace WeatherStationServer
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.Add(new ServiceRoute("api", new WebServiceHostFactory(), typeof(RestService)));

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "angular",
                url: "V2/{*catch-all}",
                defaults: new
                {
                    controller = "Angular",
                    action = "Index"
                });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new {controller = "Station", action = "Details", id = UrlParameter.Optional}
            );

            //throw new ApplicationException("Bang!");
        }
    }
}