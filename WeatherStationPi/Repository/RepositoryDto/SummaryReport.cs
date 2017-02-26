using System.Collections.Generic;

namespace Repository.RepositoryDto
{
    public class SummaryReport
    {
        public SummaryReport(IList<SensorDetails> sensorDetails, List<string> stationIds)
        {
            SensorDetails = sensorDetails;
            StationIds = stationIds;
        }

        public IList<SensorDetails> SensorDetails { get; }
        public List<string> StationIds { get; }
    }
}