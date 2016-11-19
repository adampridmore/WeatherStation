using System;
using System.Collections.Generic;
using ApiDtos.ApiDto;

namespace WeatherStationClient
{
    static class Program
    {
        static void Main()
        {
            Console.WriteLine("Hello Weather Station Client");

            var weatherStationServerApi =  new WeatherStationServerApi();

            //for (var i = 0; i < 30; i++)
            //{
            //    System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));

            //    Console.WriteLine(weatherStationServerApi.ServerDateTimeUtc());
            //}

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));

            Console.WriteLine(weatherStationServerApi.ServerDateTimeUtc());

            var dataPoints = new List<DataPoint>
            {
                new DataPoint
                {
                    SensorValueText = "My Sensor Value",
                    //TimeStampUtc = DateTime.UtcNow.ToString("o")
                }
            };
            weatherStationServerApi.SendDataPoints(dataPoints);
        }
    }
}
