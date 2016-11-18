using System.Data.Entity;
using Repository.RepositoryDto;

namespace Repository
{
    public class WeatherStationDbContext : DbContext
    {
        public WeatherStationDbContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
        }

        public DbSet<DataPoint> DataPoints { get; set; }
    }
}