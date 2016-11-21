import urllib.request
import json
import socket
from urllib.error import HTTPError, URLError


class WeatherStationServer:
    def __init__(self, url):
        self.url = url

    def try_send_data(self, points):
        postdata = {
            "DataPoints": []
        }

        for point in points:
            pointDto = self.__create_data_point_json_dto(
                point["ReadingTimeStamp"],
                point["SensorType"],
                point["SensorValue"], )
            postdata["DataPoints"].append(pointDto)

        jsondata = json.dumps(postdata)
        jsondatabytes = jsondata.encode('utf-8')

        return self.__do_post__(jsondatabytes)

    def __do_post__(self, json_data_bytes):
        req = urllib.request.Request(self.url)
        req.add_header('Content-Type', 'application/json')
        try:
            return self.__post(json_data_bytes, req)
        except (URLError, HTTPError) as e:
            print("Failed server call.", e)
            return False

    def __post(self, json_data_bytes, req):
        response = urllib.request.urlopen(req, json_data_bytes)
        response_body = response.read()
        # print('{0:15} {1:15} - {2}'.format(sensor_type, sensor_value, response_body))
        if response_body == b'"OK"':
            return True
        else:
            print("Failed server call. Body: " + str(response_body))
            return False

    def __create_data_point_json_dto(self, sensor_timestamp_utc, sensor_type, sensor_value):
        return {
            "StationId": "weatherStation1_" + socket.gethostname(),
            "SensorType": sensor_type,
            "SensorValueText": str(sensor_value),
            "SensorValueNumber": sensor_value,
            "SensorTimestampUtc": sensor_timestamp_utc
        }
