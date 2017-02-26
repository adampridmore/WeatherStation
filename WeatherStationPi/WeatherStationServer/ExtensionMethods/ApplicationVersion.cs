using System.Reflection;

namespace WeatherStationServer.ExtensionMethods
{
    public class ApplicationVersion
    {
        public static string Version()
        {
            return Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }
    }
}