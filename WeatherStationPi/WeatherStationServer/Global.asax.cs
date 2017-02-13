using System;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace WeatherStationServer
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            Bundles.RegisterBundles(BundleTable.Bundles);
        }
    }
}