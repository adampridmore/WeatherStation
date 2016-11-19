using System.Collections.Generic;
using Repository.RepositoryDto;

namespace WeatherStationServer.Model.Home
{
    public class IndexModel
    {
        public SummaryReport SummaryReport { get; set; }
        public string ChartHtml { get; set; }
        public List<string> ChartHtmlList { get; set; }
        public List<DataPoint> LatestDataPoints { get; set; }
        public string StationId { get; set; }
    }
}