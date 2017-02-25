using System.Collections.Generic;

namespace WeatherStationServer.Controllers.Api.Dto
{
    public class StationLastValues
    {
        public string StationId { get; set; }
        public List<LastValue> LastValues { get; set; }
    }
}