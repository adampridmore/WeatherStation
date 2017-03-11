using Repository.Interfaces;

namespace Repository
{
    // ReSharper disable once ClassNeverInstantiated.Global

    // TODO - Rename to DefaultMsSqlConnectionStringFactory 
    public class DefaultConnectionStringFactory : IConnectionStringFactory
    {
        public string GetNameOrConnectionString()
        {
            return "name = DefaultConnection";
        }
    }
}