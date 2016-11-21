using System;

namespace WeatherStationServer.Model.Sensor
{
    public class SensorDetailsModel
    {
        public string GuageHtml { get; set; }
        public string Timestamp { get; set; }
        public TimeSpan? ReloadTimeSpan { get; set; }
        public string StationId { get; set; }
        public string SensorType { get; set; }
    }
}