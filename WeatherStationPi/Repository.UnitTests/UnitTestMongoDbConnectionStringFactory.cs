using System;
using Repository.Interfaces;

namespace Repository.UnitTests
{
    public class UnitTestMongoDbConnectionStringFactory : IConnectionStringFactory
    {
        public string GetNameOrConnectionString()
        {
            return "mongodb://localhost/WeatherStation_unitTests";
        }
    }
}