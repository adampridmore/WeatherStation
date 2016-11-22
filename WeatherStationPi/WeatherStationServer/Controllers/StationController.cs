using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.FSharp.Core;
using Repository;
using WeatherStationServer.Model.Station;
using XPlot.GoogleCharts;

namespace WeatherStationServer.Controllers
{
    public class StationController : Controller
    {
        //GET: Home
        public ActionResult Details(string stationId, DateTime? startDateTime, DateTime? endDateTime)
        {
            var repository = new DataPointRepository();
            var summary = repository.GetSummaryReport();

            stationId = GetStattionId(summary.StationIds, stationId);

            var model = new StationDetailsModel
            {
                AllStationIds = summary.StationIds,
                StationId = stationId,
                ChartHtmlList = CreateChartHtmlList(stationId).ToList(),
                LatestDataPoints = repository.GetLastValues(stationId)
            };

            return View(model);
        }

        private static IEnumerable<string> CreateChartHtmlList(string stationId)
        {
            var repository = new DataPointRepository();

            yield return GetChartHtmlForStationSensor(stationId, repository, "Temperature");
            yield return GetChartHtmlForStationSensor(stationId, repository, "Humidity");
            yield return GetChartHtmlForStationSensor(stationId, repository, "Pressure");
        }

        private static string GetChartHtmlForStationSensor(string stationId, DataPointRepository repository, string sensorType)
        {
            var dataPoints = repository.GetDataPoints(stationId, sensorType)
                .Where(dp => dp.SensorValueNumber != 0.0d)
                .Select(dp => new Tuple<DateTime, double>(dp.SensorTimestampUtc, dp.SensorValueNumber))
                .ToList();

            if (!dataPoints.Any())
            {
                return string.Empty;
            }


            var chart = Chart.Line(dataPoints, FSharpOption<IEnumerable<string>>.None,
                FSharpOption<Configuration.Options>.None);
            chart.WithOptions(new Configuration.Options() {title = sensorType});

            return chart.GetInlineHtml();
        }

        private string GetStattionId(List<string> allStationIds, string stationId)
        {
            if (!string.IsNullOrWhiteSpace(stationId))
            {
                return stationId;
            }

            var foundStationId = allStationIds.FirstOrDefault(s => s == stationId);

            if (foundStationId == null)
            {
                return allStationIds.FirstOrDefault();
            }

            return foundStationId;
        }
    }
}