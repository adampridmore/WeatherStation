namespace ApiDtos.ApiDto
{
    public class DataPoint
    {
        public string StationId { get; set; }

        public string SensorType { get; set; }

        public string SensorValueText { get; set; }

        public double SensorValueNumber { get; set; }

        //// TODO - Lower case Timestamp
        //public string TimeStampUtc { get; set; }
    }
}