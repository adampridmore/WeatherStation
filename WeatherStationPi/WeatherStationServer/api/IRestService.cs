using System;
using System.ServiceModel;
using System.ServiceModel.Web;
using ApiDtos.ApiDto;

namespace WeatherStationServer.api
{
    // http://localhost:59653/api/json/1
    // http://calf:59653/api/json/1
    [ServiceContract]
    public interface IRestService
    {
        //[OperationContract]
        //[WebInvoke(Method = "GET",
        //     ResponseFormat = WebMessageFormat.Xml,
        //     BodyStyle = WebMessageBodyStyle.Wrapped,
        //     UriTemplate = "xml/{id}")]
        //string XmlData(string id);

        //[OperationContract]
        //[WebInvoke(Method = "GET",
        //     ResponseFormat = WebMessageFormat.Json,
        //     BodyStyle = WebMessageBodyStyle.Wrapped,
        //     UriTemplate = "json/{id}")]
        //string JsonData(string id);

        /// <summary>
        ///  http://localhost/WeatherStationServer/api/serverDateTimeUtc
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET",
             ResponseFormat = WebMessageFormat.Json,
             BodyStyle = WebMessageBodyStyle.Wrapped,
             UriTemplate = "serverDateTimeUtc")]
        DateTime ServerDateTimeUtc();

        /// <summary>
        /// POST: http://localhost:59653/api/dataPoints
        /// {
        ///   "DataPoints" : [{
        ///     "SensorValueText" : "ValueText"
        ///  }]
        ///}
        /// </summary>
        /// <param name="dataPointsRequest"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST",
             ResponseFormat = WebMessageFormat.Json,
             RequestFormat = WebMessageFormat.Json,
             BodyStyle = WebMessageBodyStyle.Bare,
             UriTemplate = "dataPoints")]
        string AddDataPoints(AddDataPointsRequest dataPointsRequest);

        /// <summary>
        ///  http://localhost/WeatherStationServer/api/dataPoints?stationId=weatherStation1_raspberrypi&sensorType=Temperature
        /// </summary>
        /// <param name="stationId"></param>
        /// <param name="sensorType"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET",
             ResponseFormat = WebMessageFormat.Json,
             RequestFormat = WebMessageFormat.Json,
             BodyStyle = WebMessageBodyStyle.Bare,
             UriTemplate = "dataPoints?stationId={stationId}&sensorType={sensorType}")]
        GetDataPointsResponse GetDataPoints(string stationId, string sensorType);

        /// <summary>
        ///  http://localhost/WeatherStationServer/api/lastStationDataPoints/weatherStation1_raspberrypi
        /// </summary>
        /// <param name="stationId"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET",
             ResponseFormat = WebMessageFormat.Json,
             RequestFormat = WebMessageFormat.Json,
             BodyStyle = WebMessageBodyStyle.Bare,
             UriTemplate = "lastStationDataPoints/{stationId}")]
        GetDataPointsResponse GetLastStationDataPoints(string stationId);
    }
}