using System.Collections.Generic;
using System.Linq;
using Repository.RepositoryDto;

namespace Repository
{
    public class DataPointRepository
    {
        public void Save(DataPoint dataPoint)
        {
            using (var context = new WeatherStationDbContext())
            {
                context.DataPoints.Add(dataPoint);

                context.SaveChanges();
            }
        }

        public IList<DataPoint> GetDataPoints(string stationId, string sensorType)
        {
            using (var context = new WeatherStationDbContext())
            {
                return context
                    .DataPoints
                    .AsQueryable()
                    .Where(dp => dp.StationId == stationId && dp.SensorType == sensorType)
                    .OrderBy(dp => dp.TimeStamp)
                    .ToList()
                    ;
            }
        }
    }
}
