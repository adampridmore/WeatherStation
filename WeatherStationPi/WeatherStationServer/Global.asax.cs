﻿using System;
using System.ServiceModel.Activation;
using System.Web.Routing;
using WeatherStationServer.api;

namespace WeatherStationServer
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            RouteTable.Routes.Add(new ServiceRoute("api", new WebServiceHostFactory(), typeof(RestService)));
        }
    }
}