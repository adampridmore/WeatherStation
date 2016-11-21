using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.FSharp.Core;
using Repository;
using Repository.RepositoryDto;
using WeatherStationServer.Model.Sensor;
using XPlot.GoogleCharts;

namespace WeatherStationServer.Controllers
{
    public class SensorController : Controller
    {
        // GET: Sensor
        public ActionResult Details(string stationId, string sensorType, int size = 300, int? reloadSeconds = null)
        {
            var repository = new DataPointRepository();

            var dataPoint = repository.GetLastValues(stationId, sensorType);

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
            {
                return null;
            }

            return TimeSpan.FromSeconds(reloadSeconds.Value);
        }

        private string CreateTempratureGuageHtml(DataPoint dataPoint, int size)
        {
            if (dataPoint == null)
            {
                return string.Empty;
            }

            var data = new List<Tuple<string, decimal>>
            {
                new Tuple<string, decimal>("C", FotmatSensorNumber(dataPoint))
            };

            var max = 30;
            var min = -10;
            var options = new Configuration.Options
            {
                yellowFrom = min,
                yellowTo = 0,
                yellowColor = "#aaaaff",
                //greenFrom = 15,
                //greenTo = 25,
                redFrom = 30,
                redTo = max,
                max = max,
                min = min,
                minorTicks = 5,
                is3D = true,
                displayAnnotations = true,
            };

            var guage = Chart.Gauge(
                data,
                FSharpOption<IEnumerable<string>>.None,
                FSharpOption<Configuration.Options>.Some(options));

            guage.WithSize(size, size);

            return guage.GetInlineHtml();
        }

        private static decimal FotmatSensorNumber(DataPoint dataPoint)
        {
            return Math.Round((decimal)dataPoint.SensorValueNumber, 1);
        }
    }
}