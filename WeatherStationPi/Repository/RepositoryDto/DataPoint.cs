using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.RepositoryDto
{
    public class DataPoint
    {
        public override string ToString()
        {
            return $"{nameof(StationId)}: {StationId}, {nameof(SensorType)}: {SensorType}, {nameof(SensorValueNumber)}: {SensorValueNumber}, {nameof(SensorTimestampUtc)}: {SensorTimestampUtc}, {nameof(ReceivedTimestampUtc)}: {ReceivedTimestampUtc}";
        }

        [Key, Column(Order = 0)]
        public string StationId { get; set; }

        [Key, Column(Order = 1)]
        public string SensorType { get; set; }

        [NotMapped]
        public SensorTypeEnum? SensorTypeEnum
        {

            get
            {
                SensorTypeEnum e;
                if (Enum.TryParse(SensorType, true, out e))
                {
                    return e;
                }
                return null;
            }
        }

        [Key, Column(Order = 2)]
        public DateTime SensorTimestampUtc { get; set; }

        public double SensorValueNumber { get; set; }

        public DateTime ReceivedTimestampUtc { get; set; }

        public static DataPoint Create(
            string stationId,
            string sensorType,
            double sensorValueNumber,
            DateTime sensorTimestampUtc,
            DateTime reveivedTimestampUtc)
        {
            return new DataPoint
            {
                StationId = stationId,
                SensorType = sensorType,
                SensorValueNumber = sensorValueNumber,
                SensorTimestampUtc = sensorTimestampUtc,
                ReceivedTimestampUtc = reveivedTimestampUtc
            };
        }

        public static DataPoint Empty()
        {
            return new DataPoint();
        }

        public static bool IdentityEquals(DataPoint dp1, DataPoint dp2)
        {
            if (dp1 == null && dp2 == null)
            {
                return true;
            }

            if (dp1 == null || dp2 == null)
            {
                return false;
            }

            if (dp1.StationId != dp2.StationId)
            {
                return false;
            }

            if (dp1.SensorType!= dp2.SensorType)
            {
                return false;
            }

            if (dp1.SensorTimestampUtc != dp2.SensorTimestampUtc)
            {
                return false;
            }

            return true;
        }
    }
}