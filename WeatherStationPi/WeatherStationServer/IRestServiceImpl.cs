using System.ServiceModel;
using System.ServiceModel.Web;

namespace WeatherStationServer
{
    [ServiceContract]
    public interface IRestServiceImpl
    {
        [OperationContract]
        [WebInvoke(Method = "GET",
             ResponseFormat = WebMessageFormat.Xml,
             BodyStyle = WebMessageBodyStyle.Wrapped,
             UriTemplate = "xml/{id}")]
        string XmlData(string id);

        [OperationContract]
        [WebInvoke(Method = "GET",
             ResponseFormat = WebMessageFormat.Json,
             BodyStyle = WebMessageBodyStyle.Wrapped,
             UriTemplate = "json/{id}")]
        string JsonData(string id);
    }
}