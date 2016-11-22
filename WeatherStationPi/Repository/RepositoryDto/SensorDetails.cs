using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.RepositoryDto
{
    public class SensorDetails
    {
        public string StationId { get; set; }
        public string SensorType { get; set; }
        public int Count { get; set; }
        public DateTime Min { get; set; }
        public DateTime Max { get; set; }

        public static IList<string> GetSensorTypeValues() => Enum.GetNames(typeof(SensorTypeEnum)).ToList();
    }
}