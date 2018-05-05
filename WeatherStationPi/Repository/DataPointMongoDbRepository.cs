using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Linq;
using Repository.Interfaces;
using Repository.RepositoryDto;
using MongoDB.Bson;

namespace Repository
{
    public class DataPointMongoDbRepository : IDataPointRepository
    {
        public DataPointMongoDbRepository(IConnectionStringFactory connectionStringFactory)
        {
            var connectionString = connectionStringFactory.GetNameOrConnectionString();
            Collection = GetCollection(connectionString);
        }

        public IMongoCollection<DataPointMongoDb> Collection { get; set; }

        public void Save(DataPoint dataPoint)
        {
            var mongoDbDataPoint = DataPointMongoDb.FromDataPoint(dataPoint);

            Collection.ReplaceOne(
                    new BsonDocument("_id", new BsonDocument
                    {
                        new BsonElement("StationId", mongoDbDataPoint.Id.StationId),
                        new BsonElement("SensorType", mongoDbDataPoint.Id.SensorType),
                        new BsonElement("SensorTimestampUtc", mongoDbDataPoint.Id.SensorTimestampUtc)
                    }),
                    mongoDbDataPoint,
                    new UpdateOptions { IsUpsert = true });
        }

        public IList<DataPoint> GetDataPoints(
            string stationId,
            string sensorType,
            DateTimeRange dateTimeRange)
        {
            var query = Collection.AsQueryable()
                .Where(dp => dp.Id.StationId == stationId)
                .Where(dp => dp.Id.SensorType == sensorType)
                ;

            query = AddDateRangeToQuery(dateTimeRange, query);

            return query
                .ToList()
                .Select(DataPointMongoDb.ToDataPoint)
                .Where(dp => DataPointSqlRepository.IsValidValue(dp))
                .ToList();
        }

        private static IMongoQueryable<DataPointMongoDb> AddDateRangeToQuery(DateTimeRange dateTimeRange, IMongoQueryable<DataPointMongoDb> query)
        {
            if (dateTimeRange.Start.HasValue)
            {
                query = query.Where(dataPoint => dataPoint.Id.SensorTimestampUtc > dateTimeRange.Start.Value);
            }
            if (dateTimeRange.End.HasValue)
            {
                query = query.Where(dataPoint => dataPoint.Id.SensorTimestampUtc < dateTimeRange.End.Value);
            }
            return query;
        }

        public SummaryReport GetSummaryReport()
        {
            throw new System.NotImplementedException();
        }

        public List<string> GetStationIds()
        {
            return Collection
                .Aggregate()
                .Group(new BsonDocument { { "_id", "$_id.StationId" } })
                .ToList()
                .Select(row => row["_id"].AsString)
                .OrderBy(x => x)
                .ToList(); ;
        }

        public DataPoint GetLastValues(string stationId, string sensorType)
        {
            throw new System.NotImplementedException();
        }

        public List<DataPoint> GetLastValues(string stationId)
        {
            throw new System.NotImplementedException();
        }

        public IList<DataPoint> FindAll()
        {
            return Collection
                .FindSync(FilterDefinition<DataPointMongoDb>.Empty)
                .ToList()
                .Select(DataPointMongoDb.ToDataPoint)
                .ToList();
        }

        public void DeleteAll()
        {
            Collection.DeleteMany(FilterDefinition<DataPointMongoDb>.Empty);
        }

        public void DeleteAllByStationId(string stationId)
        {
            Collection.DeleteMany(dp => dp.Id.StationId == stationId);
        }

        public IList<DataPoint> FindAllByStationId(string stationId)
        {
            return Collection
                .AsQueryable()
                .Where(dp => dp.Id.StationId == stationId)
                .ToList()
                .Select(DataPointMongoDb.ToDataPoint)
                .ToList();
        }

        private static IMongoCollection<DataPointMongoDb> GetCollection(string connectionString)
        {
            var url = new MongoUrl(connectionString);
            var client = new MongoClient(url);

            var database = client.GetDatabase(url.DatabaseName);

            var collection = database.GetCollection<DataPointMongoDb>("dataPoint");
            return collection;
        }
    }
}