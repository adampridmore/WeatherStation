using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Linq;
using Repository.Interfaces;
using Repository.RepositoryDto;
using static Repository.DataPointSqlRepository;

namespace Repository
{
    public class DataPointMongoDbRepository : IDataPointRepository
    {
        public DataPointMongoDbRepository(IConnectionStringFactory connectionStringFactory)
        {
            var connectionString = connectionStringFactory.GetNameOrConnectionString();
            Collection = GetCollection(connectionString);
        }

        public IMongoCollection<DataPoint> Collection { get; set; }

        public void Save(DataPoint dataPoint)
        {
            Collection.InsertOne(dataPoint);
        }

        public IList<DataPoint> GetDataPoints(
            string stationId, 
            string sensorType, 
            DateTimeRange dateTimeRange)
        {
            var query = Collection.AsQueryable()
                .Where(dp => dp.StationId == stationId)
                .Where(dp => dp.SensorType == sensorType)
                ;

            query = AddDateRangeToQuery(dateTimeRange, query);

            return query
                .ToList()
                .Where(dp => DataPointSqlRepository.IsValidValue(dp)).ToList();
        }

        private static IMongoQueryable<DataPoint> AddDateRangeToQuery(DateTimeRange dateTimeRange, IMongoQueryable<DataPoint> query)
        {
            if (dateTimeRange.Start.HasValue)
            {
                query = query.Where(dataPoint => dataPoint.SensorTimestampUtc > dateTimeRange.Start.Value);
            }
            if (dateTimeRange.End.HasValue)
            {
                query = query.Where(dataPoint => dataPoint.SensorTimestampUtc < dateTimeRange.End.Value);
            }
            return query;
        }

        public SummaryReport GetSummaryReport()
        {
            throw new System.NotImplementedException();
        }

        public List<string> GetStationIds()
        {
            throw new System.NotImplementedException();
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
                .FindSync(FilterDefinition<DataPoint>.Empty)
                .ToList();
        }

        public void DeleteAll()
        {
            Collection.DeleteMany(FilterDefinition<DataPoint>.Empty);
        }

        public void DeleteAllByStationId(string stationId)
        {
            Collection.DeleteMany(dp=>dp.StationId == stationId);
        }

        public IList<DataPoint> FindAllByStationId(string stationId)
        {
            return Collection
                .AsQueryable()
                .Where(dp => dp.StationId == stationId)
                .ToList();
        }

        private static IMongoCollection<DataPoint> GetCollection(string connectionString)
        {
            var url = new MongoUrl(connectionString);
            var client = new MongoClient(url);

            var database = client.GetDatabase(url.DatabaseName);

            var collection = database.GetCollection<DataPoint>("dataPoint");
            return collection;
        }
    }
}