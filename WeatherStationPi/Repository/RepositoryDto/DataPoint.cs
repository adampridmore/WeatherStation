using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Repository.RepositoryDto
{
    public class DataPointId
    {
        public string StationId { get; set; }

        public string SensorType { get; set; }

        public DateTime SensorTimestampUtc { get; set; }
    }
    public class DataPointMongoDb
    {
        public DataPointId Id
        {
            get;set;
        }       

        public double SensorValueNumber { get; set; }

        public DateTime ReceivedTimestampUtc { get; set; }

        public static DataPointMongoDb FromDataPoint(DataPoint dataPoint)
        {
            return new DataPointMongoDb()
            {
                Id = new DataPointId()
                {
                    SensorTimestampUtc = dataPoint.SensorTimestampUtc,
                    SensorType = dataPoint.SensorType,
                    StationId = dataPoint.StationId
                },
               ReceivedTimestampUtc = dataPoint.ReceivedTimestampUtc,
               SensorValueNumber = dataPoint.SensorValueNumber
            };
        }

        [BsonExtraElements]
        public BsonDocument AdditionalColumns { get; set; }

        public static DataPoint ToDataPoint(DataPointMongoDb dataPointMongoDb)
        {
            return DataPoint.Create(
                dataPointMongoDb.Id.StationId,
                dataPointMongoDb.Id.SensorType,
                dataPointMongoDb.SensorValueNumber,
                dataPointMongoDb.Id.SensorTimestampUtc,
                dataPointMongoDb.ReceivedTimestampUtc);
        }
    }
}