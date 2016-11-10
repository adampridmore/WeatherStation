using System;

namespace WeatherStationClient.ServerDto
{
    public class DataPoint
    {
        public string StationId { get; set; }

        public string SensorType { get; set; }

        public string SensorValueText { get; set; }

        public double SensorValueNumber { get; set; }

        // TODO - Need to handle ISO Date string -> .Net DateTime serialization
        public DateTime TimeStampUtc { get; set; }
    }
}
