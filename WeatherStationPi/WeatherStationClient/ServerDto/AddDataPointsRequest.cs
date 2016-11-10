using System.Collections.Generic;

namespace WeatherStationClient.ServerDto
{
    public class AddDataPointsRequest
    {
        public IList<DataPoint> DataPoints {get; set; }
    }
}
