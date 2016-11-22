using System.Collections.Generic;
using Repository.RepositoryDto;

namespace WeatherStationServer.Model.Station
{
    public class StationDetailsModel
    {
        public List<string> ChartHtmlList { get; set; }
        public List<DataPoint> LatestDataPoints { get; set; }
        public string StationId { get; set; }
        public List<string> AllStationIds { get; set; }
    }
}