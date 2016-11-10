using System.Collections.Generic;

namespace WeatherStationServer.api.ApiDto
{
    public class AddDataPointsRequest
    {
        public IList<DataPoint> DataPoints { get; set; }
    }
}