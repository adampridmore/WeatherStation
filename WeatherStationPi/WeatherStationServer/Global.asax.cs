﻿using System;
using System.ServiceModel.Activation;
using System.Web.Routing;

namespace WeatherStationServer
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            RouteTable.Routes.Add(new ServiceRoute("mysvc", new WebServiceHostFactory(), typeof(RestServiceImpl)));
        }
    }
}