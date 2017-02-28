using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.FSharp.Core;
using Repository.Interfaces;
using Repository.RepositoryDto;
using WeatherStationServer.Model.Sensor;
using XPlot.GoogleCharts;

namespace WeatherStationServer.Controllers
{
    public class SensorController : Controller
    {
        private readonly IDataPointRepository _repository;

        public SensorController(IDataPointRepository repository)
        {
            _repository = repository;
        }

        // GET: Station
        public ActionResult Index()
        {
            var summary = _repository.GetSummaryReport();

            var model = new SensorIndexModel
            {
                SummaryReport = summary
            };

            return View(model);
        }

        // GET: Sensor
        public ActionResult Details(string stationId, string sensorType, int size = 300, int? reloadSeconds = null)
        {
            var dataPoint = _repository.GetLastValues(stationId, sensorType);

            var model = new SensorDetailsModel
            {
                StationId = stationId,
                SensorType = sensorType,
                GuageHtml = CreateTempratureGuageHtml(dataPoint, size),
                Timestamp = dataPoint.SensorTimestampUtc.ToString("s"),
                ReloadTimeSpan = GetReloadTimeSpan(reloadSeconds)
            };

            return View(model);
        }

        private TimeSpan? GetReloadTimeSpan(int? reloadSeconds)
        {
            if (!reloadSeconds.HasValue)
                return null;

            return TimeSpan.FromSeconds(reloadSeconds.Value);
        }

        private string CreateTempratureGuageHtml(DataPoint dataPoint, int size)
        {
            if (dataPoint == null)
                return string.Empty;

            var data = new List<Tuple<string, decimal>>
            {
                new Tuple<string, decimal>(GetUnit(dataPoint.SensorTypeEnum), FotmatSensorNumber(dataPoint))
            };

            var options = CreateOptions(dataPoint.SensorTypeEnum);

            var guage = Chart.Gauge(
                data,
                FSharpOption<IEnumerable<string>>.None,
                FSharpOption<Configuration.Options>.Some(options));

            guage.WithSize(size, size);

            return guage.GetInlineHtml();
        }

        private static string GetUnit(SensorTypeEnum? sensorType)
        {
            switch (sensorType)
            {
                case SensorTypeEnum.Temperature:
                    return "C";
                case SensorTypeEnum.Humidity:
                    return "RH";
                case SensorTypeEnum.Pressure:
                    return "mb";
                default:
                    return "";
            }
        }

        private static Configuration.Options CreateOptions(SensorTypeEnum? sensorTypeEnum)
        {
            switch (sensorTypeEnum)
            {
                case SensorTypeEnum.Temperature:
                    return new Configuration.Options
                    {
                        yellowFrom = -10,
                        yellowTo = 0,
                        yellowColor = "#aaaaff",
                        redFrom = 30,
                        redTo = 30,
                        min = -10,
                        max = 30,
                        minorTicks = 5
                    };
                case SensorTypeEnum.Pressure:
                    return new Configuration.Options
                    {
                        min = 900,
                        max = 1100,
                        greenFrom = 970,
                        greenTo = 980
                    };
                default:
                    return new Configuration.Options();
            }
        }

        private static decimal FotmatSensorNumber(DataPoint dataPoint)
        {
            return Math.Round((decimal) dataPoint.SensorValueNumber, 1);
        }
    }
}