using System.Collections.Generic;

namespace WeatherStationServer.Controllers.Api.Dto
{
    public class SensorValues
    {
        public string SensorType { get; set; }
        public List<SensorValue> Values { get; set; }
    }
}