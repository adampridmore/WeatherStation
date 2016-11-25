using System.Collections.Generic;

namespace ApiDtos.ApiDto
{
    public class AddDataPointsRequest
    {
        public IList<DataPoint> DataPoints { get; set; }
    }
}