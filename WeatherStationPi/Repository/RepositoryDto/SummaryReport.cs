using System.Collections.Generic;

namespace Repository.RepositoryDto
{
    public class SummaryReport
    {
        public IList<SensorDetails> SensorDetails { get; }

        public SummaryReport(IList<SensorDetails> sensorDetails)
        {
            SensorDetails = sensorDetails;
        }
    }
}