using System.Data.Entity;

namespace Repository
{
    public class WeatherStationDbContext : DbContext
    {
        public WeatherStationDbContext() : base("server=.\\SQLEXPRESS;database=WeatherStation;Integrated Security = True; Connect Timeout = 5")
        {
        }

        public DbSet<DataPoint> DataPoints { get; set; }
    }
}
