import urllib.request
import json
import socket


class WeatherStationServer:
    # url = 'http://calf/WeatherStationServer/api/dataPoints'

    # url = 'http://localhost/WeatherStationServer/api/dataPoints'
    url = 'http://localhost:59653/api/dataPoints'

    def send_data(self, sensor_type, sensor_value):
        json_data_bytes = self.create_post_data_bytes(sensor_type, sensor_value)

        req = urllib.request.Request(self.url)
        req.add_header('Content-Type', 'application/json')

        response = urllib.request.urlopen(req, json_data_bytes)
        response_body = response.read()

        # print('{0:15} {1:15} - {2}'.format(sensor_type, sensor_value, response_body))
        if response_body == b'"OK"':
            return True
        else:
            print("Failed server call. Body: " + str(response_body))

            return False

    def create_post_data_bytes(self, sensor_type, sensor_value):
        postdata = {
            "DataPoints": [{
                "StationId": "weatherStation1_" + socket.gethostname(),
                "SensorType": sensor_type,
                "SensorValueText": str(sensor_value),
                "SensorValueNumber": sensor_value,
            }]
        }
        jsondata = json.dumps(postdata)
        jsondatabytes = jsondata.encode('utf-8')
        return jsondatabytes
