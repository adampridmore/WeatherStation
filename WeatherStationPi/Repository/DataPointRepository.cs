namespace Repository
{
    public class DataPointRepository
    {
        public void Save(DataPoint dataPoint)
        {
            var context = new WeatherStationDbContext();

            context.DataPoints.Add(dataPoint);

            context.SaveChanges();
        }
    }
}
