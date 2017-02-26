using System.Collections.Generic;

namespace WeatherStationServer.Controllers.Api.Dto
{
    public class StationIdsResponse
    {
        public StationIdsResponse(List<string> ids)
        {
            Ids = ids;
        }

        public List<string> Ids { get; set; }
    }
}