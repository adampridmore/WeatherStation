using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WeatherStationServer.Model.Home;

namespace WeatherStationServer.Controllers
{
    public class HomeController : Controller
    {
        //GET: Home
        public ActionResult Index(string stationId)
        {
            var repository = new Repository.DataPointRepository();
            var summary = repository.GetSummaryReport();

            stationId = GetStattionId(summary.StationIds, stationId);

            var model = new HomeIndexModel
            {
                AllStationIds = summary.StationIds,
                StationId = stationId,
                ChartHtmlList = WeatherCharts.getChartsHtml(stationId).ToList(),
                LatestDataPoints = repository.GetLastValues(stationId).ToList()
            };

            return View(model);
        }

        private string GetStattionId(List<string> summaryStationIds, string stationId)
        {
            var foundStationId = summaryStationIds
                .FirstOrDefault(s => s == stationId);

            if (foundStationId == null)
            {
                return summaryStationIds.FirstOrDefault();
            }

            return foundStationId;
        }
    }
}