using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;
using Repository.Interfaces;
using Repository.RepositoryDto;

namespace WeatherStationServer.Controllers.Api
{
    public class SensorApiController : WeatherStationApiControllerBase
    {
        private readonly IDataPointRepository _repository;

        public SensorApiController(IDataPointRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("api/sensor/list")]
        public JsonResult<IList<SensorDetails>> Get()
        {
            return ToJson(_repository.GetSummaryReport().SensorDetails);
        }
    }
}