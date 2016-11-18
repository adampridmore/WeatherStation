using System.Linq;
using System.Web.Mvc;
using WeatherStationServer.Model.Home;

namespace WeatherStationServer.Controllers
{
    public class HomeController : Controller
    {
        //GET: Home
        public ActionResult Index()
        {
            // TODO
            string stationId = "weatherStation1_MNAB-DEV14L";

            var repository = new Repository.DataPointRepository();
            var summary = repository.GetSummaryReport();

            var model = new IndexModel
            {
                SummaryReport = summary,
                ChartHtmlList = WeatherCharts.getChartsHtml().ToList(),
                LatestDataPoints = repository.GetLastValues(stationId)
            };

            return View(model);
        }
    }
}