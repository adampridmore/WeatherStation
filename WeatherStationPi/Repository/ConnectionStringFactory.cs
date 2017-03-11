using Repository.Interfaces;

namespace Repository
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class ConnectionStringFactory : IConnectionStringFactory
    {
        private readonly string _connectionString;

        private ConnectionStringFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public static IConnectionStringFactory CreateFromConnectionString(string connectionString  )
        {
            return new ConnectionStringFactory(connectionString);
        }

        public string GetNameOrConnectionString()
        {
            return _connectionString;
        }
    }
}