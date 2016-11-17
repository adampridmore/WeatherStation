import urllib.request
import json
import socket


class WeatherStationServer:
    # url = 'http://calf/WeatherStationServer/api/dataPoints'

    # url = 'http://localhost/WeatherStationServer/api/dataPoints'
    url = 'http://localhost:59653/api/dataPoints'

    def send_data(self, sensor_type, sensor_value):
        postdata = {
            "DataPoints": [{
                "StationId": "weatherStation1_" + socket.gethostname(),
                "SensorType": sensor_type,
                "SensorValueText": str(sensor_value),
                "SensorValueNumber": sensor_value,
            }]
        }

        req = urllib.request.Request(self.url)
        req.add_header('Content-Type', 'application/json')
        jsondata = json.dumps(postdata)
        jsondatabytes = jsondata.encode('utf-8')

        response = urllib.request.urlopen(req, jsondatabytes)
        response_body = str(response.read())

        # print(jsondata, response_body)
        # print(sensor_type, sensor_value, response_body)
        print('{0:15} {1:15} - {2}'.format(sensor_type, sensor_value, response_body))
