using System.Collections.Generic;

namespace WeatherStationServer.Controllers.Api.Dto
{
    public class LastStationData
    {
        public string StationId { get; set; }
        public List<LastValue> LastValues { get; set; }
    }
}