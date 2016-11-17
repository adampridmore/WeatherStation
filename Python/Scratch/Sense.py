from sense_hat import SenseHat
# import datetime
import urllib.request
import json
import time
import socket


def main():
    sense = SenseHat()

    while True:
        temperature = sense.get_temperature()

        send_data("Temperature", temperature)
        send_data("Humidity", sense.get_humidity())
        send_data("Pressure", sense.get_pressure())

        sense.show_message(str(int(temperature)))

        # temp = 100
        # sensor_type= "fakeTemperature"

        # send_data(sensor_type, temp)

        time.sleep(5)


def send_data(sensor_type, sensor_value):
    postdata = {
        "DataPoints": [{
            "StationId": "weatherStation1_" + socket.gethostname(),
            "SensorType": sensor_type,
            "SensorValueText": str(sensor_value),
            "SensorValueNumber": sensor_value,
        }]
    }
    url = 'http://calf/WeatherStationServer/api/dataPoints'
    # url = 'http://localhost/WeatherStationServer/api/dataPoints'
    # url = 'http://localhost:59653/api/dataPoints'
    req = urllib.request.Request(url)
    req.add_header('Content-Type', 'application/json')
    jsondata = json.dumps(postdata)
    jsondatabytes = jsondata.encode('utf-8')

    response = urllib.request.urlopen(req, jsondatabytes)
    response_body = str(response.read())

    # print(jsondata, response_body)
    # print(sensor_type, sensor_value, response_body)
    print('{0:15} {1:15} - {2}'.format(sensor_type, sensor_value, response_body))


main()
