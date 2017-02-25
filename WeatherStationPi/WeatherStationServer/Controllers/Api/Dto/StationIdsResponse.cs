using System.Collections.Generic;

namespace WeatherStationServer.Controllers.Api.Dto
{
    public class StationIdsResponse
    {
        public List<string> Ids { get; set; }

        public StationIdsResponse(List<string> ids)
        {
            Ids = ids;
        }
    }
}