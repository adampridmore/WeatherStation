using System;
using ApiDtos.ApiDto;
using Repository;

namespace WeatherStationServer.api
{
    public class RestService : IRestService
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
                System.Diagnostics.Debugger.Log(0, "", $"{dataPoint.SensorValueText}{Environment.NewLine}");
            }

            return 100;
        }

        public string Test()
        {
            var repository = new DataPointRepository();
            repository.Save(new Repository.DataPoint {SensorValueText = DateTime.UtcNow.ToString()});

            return "OK";
        }
    }
}