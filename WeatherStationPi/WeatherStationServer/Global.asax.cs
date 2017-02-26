﻿using System;
using System.Web;
using System.Web.Http;
using System.Web.Optimization;
using System.Web.Routing;

namespace WeatherStationServer
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            RouteConfig.RegisterRoutes(RouteTable.Routes);

            Bundles.RegisterBundles(BundleTable.Bundles);

            //AreaRegistration.RegisterAllAreas();
        }
    }
}