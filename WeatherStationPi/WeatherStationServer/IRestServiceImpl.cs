using System;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace WeatherStationServer
{
    // http://localhost:59653/api/json/1
    // http://calf:59653/api/json/1
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

        [OperationContract]
        [WebInvoke(Method = "GET",
             ResponseFormat = WebMessageFormat.Json,
             BodyStyle = WebMessageBodyStyle.Wrapped,
             UriTemplate = "serverDateTimeUtc")]
        DateTime ServerDateTimeUtc();
    }
}