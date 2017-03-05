using System.Collections.Generic;
using Repository.RepositoryDto;

namespace Repository.Interfaces
{
    public interface IDataPointRepository
    {
        void Save(DataPoint dataPoint);

        IList<DataPoint> GetDataPoints(
            string stationId,
            string sensorType,
            DateTimeRange dateTimeRange);

        SummaryReport GetSummaryReport();
        List<string> GetStationIds();
        DataPoint GetLastValues(string stationId, string sensorType);
        List<DataPoint> GetLastValues(string stationId);
        IList<DataPoint> FindAll();
        void DeleteAll();
        void DeleteAllByStationId(string stationId);
        IList<DataPoint> FindAllByStationId(string stationId);
    }
}