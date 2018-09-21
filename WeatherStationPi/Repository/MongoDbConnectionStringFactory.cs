using Repository.Interfaces;

namespace Repository
{
    // ReSharper disable once ClassNeverInstantiated.Global

    // TODO - Rename to DefaultMsSqlConnectionStringFactory 
    public class MongoDbConnectionStringFactory : IConnectionStringFactory
    {
        public string GetNameOrConnectionString()
        {
            return System.Configuration.ConfigurationSettings.AppSettings["MongoDbConnection"];
        }
    }
}