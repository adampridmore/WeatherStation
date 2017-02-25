namespace WeatherStationServer.ExtensionMethods
{
    public class ApplicationVersion
    {
        public static string Version()
        {
            return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }
    }
}