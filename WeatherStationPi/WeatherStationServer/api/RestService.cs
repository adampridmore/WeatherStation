using System;
using ApiDtos.ApiDto;
using Repository;
using DataPoint = Repository.RepositoryDto.DataPoint;

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
        public string AddDataPoints(AddDataPointsRequest dataPointsRequest)
        {
            throw new ApplicationException("Bang!");
            if (dataPointsRequest == null)
            {
                return "no dataPointsRequest";
            }

            if (dataPointsRequest.DataPoints == null)
            {
                return "no dataPointsRequest.DataPoints";
            }

            var repository = new DataPointRepository();

            foreach (var dataPoint in dataPointsRequest.DataPoints)
            {
                System.Diagnostics.Debugger.Log(0, "", $"{dataPoint.SensorValueText}{Environment.NewLine}");

                repository.Save(CreateDataPointRepositoryDto(dataPoint, DateTime.UtcNow));
            }
            
            return "OK";
        }

        private static DataPoint CreateDataPointRepositoryDto(ApiDtos.ApiDto.DataPoint dataPoint, DateTime now)
        {
            return DataPoint.Create(
                dataPoint.StationId,
                dataPoint.SensorType,
                dataPoint.SensorValueText,
                dataPoint.SensorValueNumber,
                now);
        }

        public string Test()
        {
            var repository = new DataPointRepository();
            repository.Save(new DataPoint {SensorValueText = DateTime.UtcNow.ToString()});

            return "OK";
        }
    }
}