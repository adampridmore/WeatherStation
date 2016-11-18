using System.Web.Mvc;

namespace WeatherStationServer.Controllers
{
    public class HomeController : Controller
    {
        //GET: Home
        public ActionResult Index()
        {

            var repository = new Repository.DataPointRepository();
            repository.GetSummaryReport();


            return View();
        }
    }
}