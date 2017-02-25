using System;

namespace WeatherStationServer.Controllers.Api.Dto
{
    public class SensorValue
    {
        public double Value { get; set; }
        public DateTime TimestampUtc { get; set; }
    }
}