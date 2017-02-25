using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Repository;
using Repository.RepositoryDto;
using WeatherStationServer.Controllers.Api.Dto;

namespace WeatherStationServer.Controllers.Api
{
    public class StationApiController : ApiController
    {
        [HttpGet]
        [Route("api/station/getIds")]
        public JsonResult<StationIdsResponse> Get()
        {
            var repository = new DataPointRepository();

            var stationIds = repository.GetStationIds();

            var data = new StationIdsResponse(stationIds);
            return ToJson(data);
        }

        [HttpGet]
        [Route("api/station/data/{stationId}")]
        public JsonResult<StationData> GetStationData(string stationId)
        {
            var repository = new DataPointRepository();
            var lastValues = repository.GetLastValues(stationId);

            var stationData = new StationData
            {
                StationId = stationId,
                LastValues = ToLastValues(lastValues)
            };

            return ToJson(stationData);
        }

        private List<LastValue> ToLastValues(List<DataPoint> lastValues)
        {
            return lastValues
                .Select(LastValue.CreateFrom)
                .ToList();
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
}