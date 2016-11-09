using System;
using RestSharp;
using WeatherStationClient.ServerDto;

namespace WeatherStationClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Weather Station Client");

            var url = "http://calf/WeatherStationServer/api/serverDateTimeUtc";

            var client = new RestClient(url);

            var request = new RestRequest(Method.GET);

            for (var i = 0; i < 30; i++)
            {
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));

                var r = client.Execute<ServerDateTimeUtcResultDto>(request);

                Console.WriteLine(r.Data.ServerDateTimeUtcResult);
            }
        }
    }
}
