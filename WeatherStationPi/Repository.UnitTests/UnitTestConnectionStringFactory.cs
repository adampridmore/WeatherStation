using Repository.Interfaces;

namespace Repository.UnitTests
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class UnitTestConnectionStringFactory : IConnectionStringFactory
    {
        public string GetNameOrConnectionString()
        {
            return @"server=.\SQLEXPRESS;database=WeatherStation_unitTests;Integrated Security = True";
        }
    }
}