using System;
using Repository.RepositoryDto;

namespace WeatherStationServer.Controllers.Api.Dto
{
    public class LastValue
    {
        public DateTime SensorTimestampUtc { get; set; }

        public double Value { get; set; }

        public string SensorType { get; set; }

        public static LastValue CreateFrom(DataPoint dataPoint)
        {
            return new LastValue
            {
                SensorType = dataPoint.SensorType,
                Value = dataPoint.SensorValueNumber,
                SensorTimestampUtc = dataPoint.SensorTimestampUtc
            };
        }
    }
}