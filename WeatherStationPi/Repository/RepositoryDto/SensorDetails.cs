using System;

namespace Repository.RepositoryDto
{
    public class SensorDetails
    {
        public string StationId { get; set; }
        public string SensorType { get; set; }
        public int Count { get; set; }
        public DateTime Min { get; set; }
        public DateTime Max { get; set; }
    }
}