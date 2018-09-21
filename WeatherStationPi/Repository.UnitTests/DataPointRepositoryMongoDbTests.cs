using Repository.Interfaces;
using SimpleInjector;
using Xunit;

namespace Repository.UnitTests
{
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

        [Fact(Skip = "Not implimented")]
        public override void GetLastValues__for_all_known_sensors_doesnt_get_unknown_types()
        {
        }

        [Fact(Skip = "Not implimented")]
        public override void GetLastValues_for_all_known_sensors()
        {
        }

        [Fact(Skip = "Not implimented")]
        public override void GetLastValues_for_sensorType_filters_on_station_and_sensor()
        {
        }

        [Fact(Skip = "Not implimented")]
        public override void GetLastValues_for_sensorType_only_get_latest()
        {
        }

        [Fact(Skip = "Not implimented")]
        public override void GetSummaryTest()
        {
        }

        [Fact(Skip = "Not implimented")]
        public override void GetSummaryTest_ordering()
        {
        }
    }
}
