using Repository.RepositoryDto;

namespace WeatherStationServer.Model.Home
{
    public class IndexModel
    {
        public SummaryReport SummaryReport { get; set; }
        public string ChartHtml { get; set; }
    }
}