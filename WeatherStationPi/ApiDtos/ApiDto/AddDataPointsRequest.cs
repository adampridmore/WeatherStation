using System.Collections.Generic;

namespace ApiDtos.ApiDto
{
    public class GetDataPointsRequest
    {
        public string StationId { get; set; }
        public string SensorType { get; set; }
    }
}