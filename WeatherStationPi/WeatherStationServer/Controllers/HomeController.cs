using System.Web.Mvc;

namespace WeatherStationServer.Controllers
{
    public class HomeController : Controller
    {
        //GET: Home
        public ActionResult Index()
        {

            var repository = new Repository.DataPointRepository();
            var summary = repository.GetSummaryReport();
            
            return View(summary);
        }
    }
}