using System.Web.Mvc;
using WeatherStationServer.Model.Home;

namespace WeatherStationServer.Controllers
{
    public class HomeController : Controller
    {
        //GET: Home
        public ActionResult Index()
        {
            var repository = new Repository.DataPointRepository();
            var summary = repository.GetSummaryReport();

            var model = new IndexModel
            {
                SummaryReport = summary,
                ChartHtmlForTemperature = WeatherCharts.getChartHtmlForTemperature()
            };
            
            return View(model);
        }
    }
}