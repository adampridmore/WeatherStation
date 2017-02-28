using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.FSharp.Core;
using Repository.Interfaces;
using Repository.RepositoryDto;
using WeatherStationServer.Model.Station;
using XPlot.GoogleCharts;

namespace WeatherStationServer.Controllers
{
    public class StationController : Controller
    {
        private readonly IDataPointRepository _repository;

        public StationController(IDataPointRepository repository)
        {
            _repository = repository;
        }

        //GET: Home
        public ActionResult Details(string stationId, string lastHours = "24")
        {
            //var dateTimeRange = DateTimeRange.Create(startDateTime, endDateTime);
            var dateTimeRange = CreateDateTimeRange(lastHours);

            
            var stationIds = _repository.GetStationIds();

            stationId = GetStationId(stationIds, stationId);

            var model = new StationDetailsModel
            {
                AllStationIds = stationIds,
                StationId = stationId,
                ChartHtmlList = CreateChartHtmlList(stationId, dateTimeRange),
                LatestDataPoints = _repository.GetLastValues(stationId)
            };

            return View(model);
        }

        public static DateTimeRange CreateDateTimeRange(string lastHours)
        {
            if (string.IsNullOrWhiteSpace(lastHours))
                return DateTimeRange.Unbounded;

            var hoursInt = int.Parse(lastHours);
            var timeSpan = TimeSpan.FromHours(hoursInt);

            return DateTimeRange.Create(DateTime.UtcNow - timeSpan, null);
        }

        private List<string> CreateChartHtmlList(string stationId, DateTimeRange dateTimeRange)
        {
            return SensorDetails
                .GetSensorTypeValues()
                .Select(sensorType => GetChartHtmlForStationSensor(stationId, _repository, sensorType, dateTimeRange))
                .Where(x => x != null)
                .ToList();
        }

        private static string GetChartHtmlForStationSensor(string stationId,
            IDataPointRepository repository,
            string sensorType,
            DateTimeRange dateTimeRange)
        {
            var dataPoints = repository.GetDataPoints(stationId, sensorType, dateTimeRange)
                .Where(dp => dp.SensorValueNumber != 0.0d)
                .Select(dp => new Tuple<DateTime, double>(dp.SensorTimestampUtc, dp.SensorValueNumber))
                .ToList();

            if (!dataPoints.Any())
                return null;

            var chart = Chart.Line(dataPoints, FSharpOption<IEnumerable<string>>.None,
                FSharpOption<Configuration.Options>.None);
            chart.WithOptions(new Configuration.Options {title = sensorType});

            return chart.GetInlineHtml();
        }

        private string GetStationId(List<string> allStationIds, string stationId)
        {
            if (!string.IsNullOrWhiteSpace(stationId))
                return stationId;

            var foundStationId = allStationIds.FirstOrDefault(s => s == stationId);

            if (foundStationId == null)
                return allStationIds.FirstOrDefault();

            return foundStationId;
        }
    }
}