﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;
using Repository.ExtensionMethods;
using Repository.Interfaces;
using Repository.RepositoryDto;
using WeatherStationServer.Controllers.Api.Dto;

namespace WeatherStationServer.Controllers.Api
{
    public class StationApiController : WeatherStationApiControllerBase
    {
        private readonly IDataPointRepository _repository;

        public StationApiController(IDataPointRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("api/station/getIds")]
        public JsonResult<StationIdsResponse> Get()
        {
            var stationIds = _repository.GetStationIds();

            var data = new StationIdsResponse(stationIds);
            return ToJson(data);
        }

        [HttpGet]
        [Route("api/station/lastValues/{stationId}")]
        public JsonResult<StationLastValues> GetStationLastValues(string stationId)
        {
            var lastValues = _repository.GetLastValues(stationId);

            var stationData = new StationLastValues
            {
                StationId = stationId,
                LastValues = ToLastValues(lastValues)
            };

            return ToJson(stationData);
        }

        [HttpGet]
        [Route("api/station/dataPoints/{stationId}")]
        public JsonResult<StationDataPoints> GetStationDataPoints(string stationId, int lastHours = 24)
        {
            var dateTimeRange = StationController.CreateDateTimeRange(lastHours.ToString());

            var sensorValuesList = SensorDetails.GetSensorTypeValues()
                    .SelectMany(type => _repository.GetDataPoints(stationId, type, dateTimeRange))
                    .GroupBy(dp => dp.SensorType)
                    .Select(ToSensorValues)
                    .ToList()
                ;

            return ToJson(new StationDataPoints
            {
                StationId = stationId,
                SensorValues = sensorValuesList
            });
        }

        private SensorValues ToSensorValues(IGrouping<string, DataPoint> grouping)
        {
            return new SensorValues
            {
                SensorType = grouping.Key,
                Values = grouping.Select(DataPointToSensorValue).ToList()
            };
        }

        private SensorValue DataPointToSensorValue(DataPoint dataPoint)
        {
            return new SensorValue
            {
                V = Math.Round(dataPoint.SensorValueNumber, 2),
                T = dataPoint.SensorTimestampUtc.GetCurrentUnixTimestampMillis() // ToString("s")
            };
        }

        private List<LastValue> ToLastValues(List<DataPoint> lastValues)
        {
            return lastValues
                .Select(LastValue.CreateFrom)
                .ToList();
        }
    }
}