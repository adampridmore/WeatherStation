using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Repository.RepositoryDto;

namespace Repository
{
    public class DataPointRepository
    {
        private readonly string _nameOrConnectionString;

        public DataPointRepository() : this("name = DefaultConnection")
        {
            
        }

        public DataPointRepository(string nameOrConnectionString)
        {
            _nameOrConnectionString = nameOrConnectionString;
        }

        public void Save(DataPoint dataPoint)
        {
            using (var context = new WeatherStationDbContext(_nameOrConnectionString))
            {
                context.DataPoints.Add(dataPoint);

                context.SaveChanges();
            }
        }

        public IList<DataPoint> GetDataPoints(string stationId, string sensorType)
        {
            using (var context = new WeatherStationDbContext(_nameOrConnectionString))
            {
                return context
                        .DataPoints.AsQueryable()
                        .Where(dataPoint => dataPoint.StationId == stationId && dataPoint.SensorType == sensorType)
                        .OrderBy(dp => dp.SensorTimestampUtc)
                        .ToList()
                    ;
            }
        }

        public SummaryReport GetSummaryReport()
        {
            using (var context = new WeatherStationDbContext(_nameOrConnectionString))
            {
                const string sql = @"
SELECT StationId,SensorType, COUNT(*) AS Count, MIN(SensorTimestampUtc) AS Min,MAX(SensorTimestampUtc) AS Max
FROM DataPoints
GROUP BY StationId, SensorType
ORDER BY StationId, SensorType";

                var results = context.Database.SqlQuery<SensorDetails>(sql);

                var sensorDetails = results.ToList();

                var stationIds = sensorDetails
                    .GroupBy(s => s.StationId)
                    .Select(g => g.Key)
                    .ToList();

                return new SummaryReport(sensorDetails, stationIds);
            }
        }

        public List<DataPoint> GetLastValues(string stationId)
        {
            using (var context = new WeatherStationDbContext(_nameOrConnectionString))
            {
                return new List<DataPoint>
                {
                    GetLatestDataPointForStationIdAndSensorType(context, stationId, "Temperature"),
                    GetLatestDataPointForStationIdAndSensorType(context, stationId, "Humidity"),
                    GetLatestDataPointForStationIdAndSensorType(context, stationId, "Pressure")
                };
            }
        }

        private static DataPoint GetLatestDataPointForStationIdAndSensorType(
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

            if (dataPoint == null)
            {
                return DataPoint.Empty();
            }

            return dataPoint;
        }
    }
}
