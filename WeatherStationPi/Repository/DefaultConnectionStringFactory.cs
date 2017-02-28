using Repository.Interfaces;

namespace Repository
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class DefaultConnectionStringFactory : IConnectionStringFactory
    {
        public string GetNameOrConnectionString()
        {
            return "name = DefaultConnection";
        }
    }
}