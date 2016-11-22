using System.Web.Mvc;
using WeatherStationServer.Model.Station;

namespace WeatherStationServer.Controllers
{
    public class StationController : Controller
    {
        // GET: Station
        public ActionResult Index()
        {
            var repository = new Repository.DataPointRepository();
            var summary = repository.GetSummaryReport();

            var model = new StationIndexModel
            {
                SummaryReport = summary
            };

            return View(model);
        }
    }
}