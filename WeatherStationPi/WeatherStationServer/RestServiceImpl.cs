namespace WeatherStationServer
{
    public class RestServiceImpl : IRestServiceImpl
    {
        public string XmlData(string id)
        {
            return "Request id: " + id;
        }

        public string JsonData(string id)
        {
            return "Request id: " + id;
        }   
    }
}