using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Repository;
using Repository.ExtensionMethods;
using Repository.RepositoryDto;
using WeatherStationServer.Controllers.Api.Dto;

namespace WeatherStationServer.Controllers.Api
{
    public class SensorApiController : WeatherStationApiControllerBase
    {
        [HttpGet]
        [Route("api/sensor/list")]
        public JsonResult<IList<SensorDetails>> Get()
        {
            var repository = new DataPointRepository();

            return ToJson(repository.GetSummaryReport().SensorDetails);
        }
    }
}