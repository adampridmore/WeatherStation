using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using Xunit;
using FluentAssertions;
using Repository;
using Repository.Interfaces;
using RestSharp;
using SimpleInjector;

namespace WeatherStationServer.ApiTests
{
    [Trait("Category", "ApiTests")]
    public class ApiTests
    {
        private string _apiTestsStationId = "apiTestsStationId";

        [Fact]
        public void Get_serverDateTime()
        {
            var url = "http://localhost:59653/api/";

            var client = new RestClient(url);

            client.AddDefaultHeader(HttpRequestHeader.Accept.ToString(), "application/json");
            client.AddDefaultHeader(HttpRequestHeader.ContentType.ToString(), "application/json");

            var request = new RestRequest("serverDateTimeUtc", Method.GET);

            var response = client.Execute(request);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var responseData = JsonConvert.DeserializeObject<dynamic>(response.Content);
            DateTime serverDateTimeUtcResult = responseData.ServerDateTimeUtcResult;

            serverDateTimeUtcResult.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
        }

        [Fact]
        public void Post_dataPoints()
        {
            var repository = CreateContainer().GetInstance<DataPointSqlRepository>();

            repository.DeleteAllByStationId(_apiTestsStationId);

            var url = "http://localhost:59653/api/";

            var client = new RestClient(url);

            client.AddDefaultHeader(HttpRequestHeader.Accept.ToString(), "application/json");
            client.AddDefaultHeader(HttpRequestHeader.ContentType.ToString(), "application/json");

            var request = new RestRequest("dataPoints", Method.POST);

            dynamic postData = new
            {
                DataPoints = new List<dynamic>
                {
                    new
                    {
                        StationId = _apiTestsStationId,
                        SensorType = "mySensorType",
                        SensorValueNumber = "123",
                        SensorTimestampUtc = "2001-02-03T12:30:45Z"
                    }
                }
            };

            request.AddJsonBody(postData);

            var response = client.Post(request);
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            response.Content.Should().Be(@"""OK""");

            var allDataPoints = repository.FindAllByStationId(_apiTestsStationId);
            allDataPoints.Should().HaveCount(1);
            allDataPoints[0].StationId.Should().Be(_apiTestsStationId);
            allDataPoints[0].SensorType.Should().Be("mySensorType");
            allDataPoints[0].SensorValueNumber.Should().Be(123);
            allDataPoints[0].SensorTimestampUtc.Should().Be(new DateTime(2001,2,3,12,30,45, DateTimeKind.Utc));
            allDataPoints[0].ReceivedTimestampUtc.Should().BeCloseTo(DateTime.UtcNow,TimeSpan.FromSeconds(5));
        }

        public Container CreateContainer()
        {
            var container = new Container();

            container.Register<IDataPointRepository, DataPointSqlRepository>();
            container.Register<IConnectionStringFactory, ApiTestConnectionStringFactory>();

            container.Verify();
            return container;
        }
    }

    public class ApiTestConnectionStringFactory : IConnectionStringFactory
    {
        public string GetNameOrConnectionString()
        {
            return @"server=.\SQLEXPRESS;database=WeatherStation;Integrated Security = True; Connect Timeout = 5";
        }
    }
}