using System.Collections.Generic;
using System.Runtime.Serialization;

namespace WeatherStationServer.api.ApiDto
{
    [DataContract]
    public class AddDataPointsRequest
    {
        [DataMember]
        public IList<DataPoint> DataPoints { get; set; }
    }
}