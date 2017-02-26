namespace WeatherStationServer.Model
{
    public class SensorModel
    {
        public SensorModel(string stationId, string sensorType)
        {
            StationId = stationId;
            SensorType = sensorType;
        }

        public string StationId { get; }
        public string SensorType { get; }
    }
}