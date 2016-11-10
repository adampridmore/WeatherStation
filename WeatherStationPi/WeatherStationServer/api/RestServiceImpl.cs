using System;
using System.Collections.Generic;
using System.ServiceModel;
using WeatherStationServer.api.ApiDto;

namespace WeatherStationServer.api
{
    public class RestServiceImpl : IRestServiceImpl
    {
        public string XmlData(string id)
        {
            return "Request id: " + id;
        }

        public string JsonData(string id)
        {
            return "Request id: " + id;
        }

        public DateTime ServerDateTimeUtc()
        {
            return DateTime.UtcNow;
        }

        // POST: http://localhost:59653/api/dataPoints
        /* {
        	"DataPoints" : [{
    		    "SensorValueText" : "ValueText"
        	    }]
            }
        */
        public int AddDataPoints(AddDataPointsRequest dataPointsRequest)
        {
            if (dataPointsRequest == null)
            {
                return -1;
            }

            if (dataPointsRequest.DataPoints == null)
            {
                return -2;
            }

            foreach (var dataPoint in dataPointsRequest.DataPoints)
            {
                System.Diagnostics.Debugger.Log(0, "", dataPoint.SensorValueText);
            }

            return 100;
        }
    }
}