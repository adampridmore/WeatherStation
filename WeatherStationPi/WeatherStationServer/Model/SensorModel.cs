using System;
using Repository.RepositoryDto;

namespace WeatherStationServer.Model
{
    public class SensorModel
    {
        public string StationId { get; }
        public string SensorType { get; }

        public SensorModel(string stationId, string sensorType)
        {
            StationId = stationId;
            SensorType = sensorType;
        }
    }
}