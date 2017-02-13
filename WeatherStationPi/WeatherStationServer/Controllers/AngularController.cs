using System.Web.Mvc;

namespace WeatherStationServer.Controllers
{
    public class AngularController : Controller
    {
        // GET: Angular
        public ActionResult Index()
        {
            return View();
        }
    }
}