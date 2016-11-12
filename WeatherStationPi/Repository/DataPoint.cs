using System;

namespace Repository
{
    public class DataPoint
    {
        public int Id { get; set; }
        public string StationId { get; set; }

        public string SensorType { get; set; }

        public string SensorValueText { get; set; }

        public double SensorValueNumber { get; set; }

        public DateTime? TimeStamp { get; set; }
    }
}
