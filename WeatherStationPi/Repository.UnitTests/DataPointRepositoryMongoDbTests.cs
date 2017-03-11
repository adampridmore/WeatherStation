using NUnit.Framework;
using Repository.Interfaces;
using SimpleInjector;

namespace Repository.UnitTests
{
    [Ignore("Not implemented")]
    public class DataPointRepositoryMongoDbTests : DataPointRepositoryTests
    {
        protected override Container CreateContainer()
        {
                var container = new Container();

                container.Register<IDataPointRepository, DataPointMongoDbRepository>();
                container.Register<IConnectionStringFactory, UnitTestMongoDbConnectionStringFactory>();

                container.Verify();
                return container;
        }
    }
}
