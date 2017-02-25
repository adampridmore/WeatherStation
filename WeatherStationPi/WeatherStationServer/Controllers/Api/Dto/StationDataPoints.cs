using System.Collections.Generic;

namespace WeatherStationServer.Controllers.Api.Dto
{
    public class StationDataPoints
    {
        public string StationId { get; set; }
        public List<SensorValues> SensorValues { get; set; }
    }
}