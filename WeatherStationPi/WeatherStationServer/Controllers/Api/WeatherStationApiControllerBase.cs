using System.Web.Http;
using System.Web.Http.Results;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace WeatherStationServer.Controllers.Api
{
    public abstract class WeatherStationApiControllerBase : ApiController
    {
        protected JsonResult<T> ToJson<T>(T data)
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            return Json(data, settings);
        }
    }
}