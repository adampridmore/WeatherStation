using System;
using System.Collections;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using WeatherStationServer.api.ApiDto;

namespace WeatherStationServer.api
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


        [OperationContract]
        [WebInvoke(Method = "POST",
             ResponseFormat = WebMessageFormat.Json,
             RequestFormat = WebMessageFormat.Json,
             BodyStyle = WebMessageBodyStyle.Bare,
             UriTemplate = "dataPoints")]
        int AddDataPoints(AddDataPointsRequest dataPointsRequest);
    }
}