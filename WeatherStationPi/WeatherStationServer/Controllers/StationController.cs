using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WeatherStationServer.Model.Station;

namespace WeatherStationServer.Controllers
{
    public class StationController : Controller
    {
        //GET: Home
        public ActionResult Details(string stationId)
        {
            var repository = new Repository.DataPointRepository();
            var summary = repository.GetSummaryReport();

            stationId = GetStattionId(summary.StationIds, stationId);

            var model = new StationDetailsModel
            {
                AllStationIds = summary.StationIds,
                StationId = stationId,
                ChartHtmlList = WeatherCharts.getChartsHtml(stationId).ToList(),
                LatestDataPoints = repository.GetLastValues(stationId).ToList()
            };

            return View(model);
        }

        private string GetStattionId(List<string> allStationIds, string stationId)
        {
            var foundStationId = allStationIds
                .FirstOrDefault(s => s == stationId);

            if (foundStationId == null)
            {
                return allStationIds.FirstOrDefault();
            }

            return foundStationId;
        }
    }
}