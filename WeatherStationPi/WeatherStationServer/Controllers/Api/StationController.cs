using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace WeatherStationServer.Controllers.Api
{
    public class StationController : ApiController
    {
        [HttpGet]
        public JsonResult<StationIdsResponse> Get()
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            return Json(new StationIdsResponse(new List<string> {"1", "2", "3"}), settings);
        }
    }

    public class StationIdsResponse
    {
        public List<string> Ids { get; set; }

        public StationIdsResponse(List<string> ids)
        {
            Ids = ids;
        }
    }
}
