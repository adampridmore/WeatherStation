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
        [Route("api/station/getIds")]
        public JsonResult<StationIdsResponse> Get()
        {
            var data = new StationIdsResponse(new List<string> {"1", "2", "3"});
            return ToJson(data);
        }

        [HttpGet]
        [Route("api/station/data/{stationId}")]
        public JsonResult<StationData> GetData(string stationId)
        {
            return ToJson(new StationData {StationId = stationId});
        }
        private JsonResult<T> ToJson<T>(T data)
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            return Json(data, settings);
        }
    }

    public class StationData
    {
        public string StationId { get; set; }
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