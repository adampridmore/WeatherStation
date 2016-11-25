using System.Collections.Generic;

namespace ApiDtos.ApiDto
{
    public class GetDataPointsResponse
    {
        public IList<DataPoint> DataPoints { get; set; }
    }
}