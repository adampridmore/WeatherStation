using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Repository;
using Repository.RepositoryDto;

namespace WeatherStationServer.Controllers
{
    public class AboutController : Controller
    {
        // GET: About
        public string Index()
        {
            return "Abount Page";
        }

        public string CreateTestData()
        {
            if (!Request.IsLocal)
                throw new HttpException(401, "Unauthorized access");

            var repository = new DataPointRepository();

            var stationId = "myTestStation";
            var sensorType = "Temperature";
            var startTime = new DateTime(2016, 1, 1);

            repository.DeleteAllByStationId(stationId);

            var points = Enumerable
                .Range(1, 5000)
                .Select(i => new DataPoint
                {
                    ReceivedTimestampUtc = DateTime.UtcNow,
                    SensorTimestampUtc = startTime + TimeSpan.FromMinutes(15*i),
                    SensorType = sensorType,
                    StationId = stationId,
                    SensorValueNumber = ToSensorValue(i)
                });

            foreach (var dataPoint in points)
                repository.Save(dataPoint);

            return "OK";
        }

        private double ToSensorValue(int i)
        {
            return Math.Sin(i/100.0*Math.PI)*10 + 10;
        }
    }
}