﻿using System;

namespace Repository.RepositoryDto
{
    public class DataPoint
    {
        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(StationId)}: {StationId}, {nameof(SensorType)}: {SensorType}, {nameof(SensorValueText)}: {SensorValueText}, {nameof(SensorValueNumber)}: {SensorValueNumber}, {nameof(TimeStamp)}: {TimeStamp}";
        }

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
