using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using ApiDtos.ApiDto;
using RestSharp;
using WeatherStationClient.ServerDto;

namespace WeatherStationClient
{
    public class WeatherStationServerApi
    {
        private readonly RestClient _client;

        public WeatherStationServerApi()
        {
            //var url = "http://calf/WeatherStationServer/api/serverDateTimeUtc";
            //var url = "http://localhost:59653/api/";

            var url = ConfigurationManager.AppSettings["WeatherStationServeerUrl"];

            _client = new RestClient(url);

            _client.AddDefaultHeader(HttpRequestHeader.Accept.ToString(), "application/json");
            _client.AddDefaultHeader(HttpRequestHeader.ContentType.ToString(), "application/json");
        }

        public DateTime ServerDateTimeUtc()
        {
            var request = new RestRequest("serverDateTimeUtc", Method.GET);

            var response = _client.Execute<ServerDateTimeUtcResultDto>(request);

            return response.Data.ServerDateTimeUtcResult;
        }

        public void SendDataPoints(IList<DataPoint> dataPoints)
        {
            var request = new RestRequest("dataPoints", Method.POST);

            var requestData = new AddDataPointsRequest
            {
                DataPoints = dataPoints
            };

            request.AddJsonBody(requestData);

            var url = _client.BuildUri(request);
            System.Diagnostics.Debugger.Log(0, "", url.ToString());

            var response = _client.Execute(request);

            Console.WriteLine(response.Content);
        }
    }
}