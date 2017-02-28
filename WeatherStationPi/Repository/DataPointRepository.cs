using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;
using Repository.Interfaces;
using Repository.RepositoryDto;

namespace Repository
{
    public class DataPointRepository : IDataPointRepository
    {
        private readonly string _nameOrConnectionString;
        
        public DataPointRepository(IConnectionStringFactory connectionStringFactory)
        {
            _nameOrConnectionString = connectionStringFactory.GetNameOrConnectionString();
        }

        public void Save(DataPoint dataPoint)
        {
            using (var context = CreateContext())
            {
                context.DataPoints.AddOrUpdate(dataPoint);

                context.SaveChanges();
            }
        }

        public IList<DataPoint> GetDataPoints(
            string stationId,
            string sensorType,
            DateTimeRange dateTimeRange)
        {
            using (var context = CreateContext())
            {
                var query = context.DataPoints
                    .AsQueryable()
                    .Where(dataPoint => dataPoint.StationId == stationId)
                    .Where(dataPoint => dataPoint.SensorType == sensorType);

                query = AddDateRangeToQuery(dateTimeRange, query);

                return query.OrderBy(dp => dp.SensorTimestampUtc).ToList();
            }
        }

        private static IQueryable<DataPoint> AddDateRangeToQuery(DateTimeRange dateTimeRange, IQueryable<DataPoint> query)
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
            using (var context = CreateContext())
            {
                var query =
                        context
                            .DataPoints
                            .AsQueryable()
                            .GroupBy(dp => new {dp.StationId, dp.SensorType})
                            .Select(g => new SensorDetails
                            {
                                StationId = g.Key.StationId,
                                SensorType = g.Key.SensorType,
                                Count = g.Count(),
                                Max = g.Max(dp => dp.SensorTimestampUtc),
                                Min = g.Min(dp => dp.SensorTimestampUtc)
                            })
                            .OrderBy(dp => new {dp.StationId, dp.SensorType})
                    ;

                var sensorDetails = query.ToList();
                var stationIds = sensorDetails.GroupBy(s => s.StationId).Select(g => g.Key).ToList();

                return new SummaryReport(sensorDetails, stationIds);
            }
        }

        public List<string> GetStationIds()
        {
            using (var context = CreateContext())
            {
                var query = context
                    .DataPoints
                    .AsQueryable()
                    .GroupBy(d => d.StationId)
                    .Select(g => g.Key);

                return query.ToList();
            }
        }

        private WeatherStationDbContext CreateContext()
        {
            return new WeatherStationDbContext(_nameOrConnectionString);
        }

        public DataPoint GetLastValues(string stationId, string sensorType)
        {
            using (var context = new WeatherStationDbContext(_nameOrConnectionString))
            {
                return TryGetLatestDataPointForStationIdAndSensorType(context, stationId, sensorType);
            }
        }

        public List<DataPoint> GetLastValues(string stationId)
        {
            using (var context = new WeatherStationDbContext(_nameOrConnectionString))
            {
                return SensorDetails
                    .GetSensorTypeValues()
                    .Select(sensorType => TryGetLatestDataPointForStationIdAndSensorType(context, stationId, sensorType))
                    .Where(dp => dp != null)
                    .ToList();
            }
        }

        private static DataPoint TryGetLatestDataPointForStationIdAndSensorType(
            WeatherStationDbContext context,
            string stationId,
            string sensorType)
        {
            const string sql = @"SELECT TOP 1 *
FROM DataPoints
WHERE StationId = @stationId AND SensorType = @sensorType
ORDER BY SensorTimestampUtc DESC";

            var dataPoint = context.DataPoints
                .SqlQuery(sql, new SqlParameter("stationId", stationId), new SqlParameter("sensorType", sensorType))
                .FirstOrDefault();

            return dataPoint;
        }

        public IList<DataPoint> FindAll()
        {
            using (var context = CreateContext())
            {
                return context.DataPoints.AsQueryable().ToList();
            }
        }

        public void DeleteAll()
        {
            using (var context = CreateContext())
            {
                context.DataPoints.RemoveRange(context.DataPoints.AsQueryable());
                context.SaveChanges();
            }
        }

        public void DeleteAllByStationId(string stationId)
        {
            using (var context = CreateContext())
            {
                var query = context.DataPoints
                    .AsQueryable()
                    .Where(dp => dp.StationId == stationId);

                context.DataPoints.RemoveRange(query);
                context.SaveChanges();
            }
        }
    }
}