using System;

namespace Repository.RepositoryDto
{
    public class DataPoint
    {
        public int Id { get; set; }
        public string StationId { get; set; }

        public string SensorType { get; set; }

        public string SensorValueText { get; set; }

        public double SensorValueNumber { get; set; }

        // TODO - Lower case Timestamp
        public DateTime? TimeStamp { get; set; }

        public static DataPoint Create(string stationId,
            string sensorType,
            string sensorValueText,
            double sensorValueNumber,
            DateTime timeStamp)
        {
            return new DataPoint
            {
                StationId = stationId,
                SensorType = sensorType,
                SensorValueText = sensorValueText,
                SensorValueNumber = sensorValueNumber,
                TimeStamp = timeStamp
            };
        }
    }
}
