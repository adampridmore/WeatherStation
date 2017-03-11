using Repository.Interfaces;
using SimpleInjector;

namespace Repository.UnitTests
{
    public class DataPointRepositorySqlTests : DataPointRepositoryTests
    {
        protected override Container CreateContainer()
        {
            var container = new Container();

            container.Register<IDataPointRepository, DataPointSqlRepository>();
            container.Register<IConnectionStringFactory, UnitTestSqlConnectionStringFactory>();

            container.Verify();
            return container;
        }
    }
}
