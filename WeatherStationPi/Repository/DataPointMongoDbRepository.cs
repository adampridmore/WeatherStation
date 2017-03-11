using System.Collections.Generic;
using Repository.Interfaces;
using Repository.RepositoryDto;

namespace Repository
{
    public class DataPointMongoDbRepository : IDataPointRepository
    {
        public void Save(DataPoint dataPoint)
        {
            throw new System.NotImplementedException();
        }

        public IList<DataPoint> GetDataPoints(string stationId, string sensorType, DateTimeRange dateTimeRange)
        {
            throw new System.NotImplementedException();
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
            throw new System.NotImplementedException();
        }

        public void DeleteAll()
        {
            throw new System.NotImplementedException();
        }

        public void DeleteAllByStationId(string stationId)
        {
            throw new System.NotImplementedException();
        }

        public IList<DataPoint> FindAllByStationId(string stationId)
        {
            throw new System.NotImplementedException();
        }
    }
}