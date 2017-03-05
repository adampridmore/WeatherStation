using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using NUnit.Framework;
using Repository;
using Repository.Interfaces;
using RestSharp;
using SimpleInjector;

namespace WeatherStationServer.ApiTests
{
    [TestFixture]
    [Category("ApiTests")]
    public class ApiTests
    {
        [Test]
        public void Get_serverDateTime()
        {
            var url = "http://localhost:59653/api/";

            var client = new RestClient(url);

            client.AddDefaultHeader(HttpRequestHeader.Accept.ToString(), "application/json");
            client.AddDefaultHeader(HttpRequestHeader.ContentType.ToString(), "application/json");

            var request = new RestRequest("serverDateTimeUtc", Method.GET);

            var response = client.Execute(request);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var responseData = JsonConvert.DeserializeObject<dynamic>(response.Content);
            DateTime serverDateTimeUtcResult = responseData.ServerDateTimeUtcResult;

            Assert.That(serverDateTimeUtcResult, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(5)));
        }

        [Test]
        public void Post_dataPoints()
        {
            var repository = CreateContainer().GetInstance<DataPointRepository>();

            repository.DeleteAll();

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
                        StationId = "myStationId",
                        SensorType = "mySensorType",
                        SensorValueNumber = "123",
                        SensorTimestampUtc = "2001-02-03T12:30:45Z"
                    }
                }
            };

            request.AddJsonBody(postData);

            var response = client.Post(request);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            Assert.That(response.Content, Is.EqualTo(@"""OK"""));

            var allDataPoints = repository.FindAll();
            Assert.That(allDataPoints, Has.Length.EqualTo(1));
            Assert.That(allDataPoints[0].StationId, Is.EqualTo("myStationId"));
            Assert.That(allDataPoints[0].SensorType, Is.EqualTo("mySensorType"));
            Assert.That(allDataPoints[0].SensorValueNumber, Is.EqualTo(123));
            Assert.That(allDataPoints[0].SensorTimestampUtc, Is.EqualTo(new DateTime(2001,2,3,12,30,45, DateTimeKind.Utc)));
            Assert.That(allDataPoints[0].ReceivedTimestampUtc, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(5)));
        }

        public Container CreateContainer()
        {
            var container = new Container();

            container.Register<IDataPointRepository, DataPointRepository>();
            container.Register<IConnectionStringFactory, ApiTestConnectionStringFactory>();

            container.Verify();
            return container;
        }
    }

    public class ApiTestConnectionStringFactory : IConnectionStringFactory
    {
        public string GetNameOrConnectionString()
        {
            return @"server=.\SQLEXPRESS;database=WeatherStation_ApiTests;Integrated Security = True; Connect Timeout = 5";
        }
    }
}