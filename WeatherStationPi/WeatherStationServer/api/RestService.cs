using System;
using System.Diagnostics;
using System.Linq;
using ApiDtos.ApiDto;
using Repository;
using Repository.Interfaces;
using Repository.RepositoryDto;
using DataPoint = ApiDtos.ApiDto.DataPoint;

namespace WeatherStationServer.api
{
    public class RestService : IRestService
    {
        private readonly IDataPointRepository _repository;

        //public string XmlData(string id)
        //{
        //    return "Request id: " + id;
        //}

        //public string JsonData(string id)
        //{
        //    return "Request id: " + id;
        //}

        public RestService()
        {
            _repository = new DataPointRepository(new DefaultConnectionStringFactory());
        }

        public DateTime ServerDateTimeUtc()
        {
            return DateTime.UtcNow;
        }

        public string AddDataPoints(AddDataPointsRequest dataPointsRequest)
        {
            if (dataPointsRequest == null)
                return "no dataPointsRequest";

            if (dataPointsRequest.DataPoints == null)
                return "no dataPointsRequest.DataPoints";

            foreach (var dataPoint in dataPointsRequest.DataPoints)
            {
                Debugger.Log(0, "", $"{dataPoint.SensorValueNumber}{Environment.NewLine}");

                _repository.Save(CreateDataPointRepositoryDto(dataPoint, DateTime.UtcNow));
            }

            return "OK";
        }

        public GetDataPointsResponse GetDataPoints(string stationId, string sensorType)
        {
            var dataPoints = _repository.GetDataPoints(
                stationId,
                sensorType,
                DateTimeRange.Last24Hours);

            return new GetDataPointsResponse {DataPoints = dataPoints.Select(ToDataPointApi).ToList()};
        }

        public GetDataPointsResponse GetLastStationDataPoints(string stationId)
        {
            var lastValues = _repository.GetLastValues(stationId);

            return new GetDataPointsResponse
            {
                DataPoints = lastValues.Select(ToDataPointApi).ToList()
            };
        }

        private DataPoint ToDataPointApi(Repository.RepositoryDto.DataPoint arg)
        {
            return new DataPoint
            {
                StationId = arg.StationId,
                SensorTimestampUtc = arg.SensorTimestampUtc.ToString("o"),
                SensorType = arg.SensorType,
                SensorValueNumber = arg.SensorValueNumber
            };
        }

        private static Repository.RepositoryDto.DataPoint CreateDataPointRepositoryDto(DataPoint dataPoint, DateTime now)
        {
            return Repository.RepositoryDto.DataPoint.Create(
                dataPoint.StationId,
                dataPoint.SensorType,
                dataPoint.SensorValueNumber,
                DateTime.Parse(dataPoint.SensorTimestampUtc),
                now);
        }
    }
}