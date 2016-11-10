namespace WeatherStationServer.api.ApiDto
{
    public class DataPoint
    {
        public string StationId { get; set; }

        public string SensorType { get; set; }

        public string SensorValueText { get; set; }

        public double SensorValueNumber { get; set; }

        public string TimeStampUtc { get; set; }
    }
}