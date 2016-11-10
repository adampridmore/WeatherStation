using System.Runtime.Serialization;

namespace WeatherStationServer.api.ApiDto
{
    [DataContract]
    public class DataPoint
    {
        [DataMember]
        public string StationId { get; set; }

        [DataMember]
        public string SensorType { get; set; }

        [DataMember]
        public string SensorValueText { get; set; }

        [DataMember]
        public double SensorValueNumber { get; set; }

        [DataMember]
        public string TimeStampUtc { get; set; }
    }
}