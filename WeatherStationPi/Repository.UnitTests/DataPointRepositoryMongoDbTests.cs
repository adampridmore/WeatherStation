using Xunit;
using Repository.Interfaces;
using SimpleInjector;

namespace Repository.UnitTests
{
	// Commented out as xunit doesn't let you ignore this fixture.    //public class DataPointRepositoryMongoDbTests : DataPointRepositoryTests
    //{
    //    protected override Container CreateContainer()
    //    {
    //            var container = new Container();

    //            container.Register<IDataPointRepository, DataPointMongoDbRepository>();
    //            container.Register<IConnectionStringFactory, UnitTestMongoDbConnectionStringFactory>();

    //            container.Verify();
    //            return container;
    //    }
    //}
}
