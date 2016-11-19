import SensorDataRepository
import datetime
import time
import WeatherStationServer
import sys


def is_test_mode():
    return (len(sys.argv) > 1) and (sys.argv[1] == "-test")


if is_test_mode():
    import SensorModuleMock as SensorModule

    defaultPollTime = 5
else:
    import SensorModule as SensorModule

    defaultPollTime = 300  # 5 minutes

weatherStationServerUrl = 'http://calf/WeatherStationServer/api/dataPoints'

# weatherStationServerUrl = 'http://localhost/WeatherStationServer/api/dataPoints'
# weatherStationServerUrl = 'http://localhost:59653/api/dataPoints'
# weatherStationServerUrl = 'http://weatherstat.azurewebsites.net/api/dataPoints'

print("Server URL:" + weatherStationServerUrl)


def main():
    repository = SensorDataRepository.SensorDataRepository()
    sensor = SensorModule.SensorModule()
    weather_station_server = WeatherStationServer.WeatherStationServer(weatherStationServerUrl)

    sensor.show_verion()

    repository.create_tables()

    # repository.delete_all();

    while True:
        temp = sensor.get_temperature()
        sensor.show_message("{}c".format(round(temp, 1)))

        collect_and_send_data(repository, sensor, weather_station_server)

        time.sleep(defaultPollTime)


def collect_and_send_data(repository, sensor, weather_station_server):
    collect_and_save_sensor_data(repository, sensor)

    try_send_data(repository, weather_station_server)

    # repository.print_all_rows()
    print(repository.get_data_stats())


def try_send_data(repository, weather_station_server):
    now = datetime.datetime.now()
    sent_count = 0
    error_count = 0

    for point in repository.get_unsent_data():
        # TODO - Send the client's timestamp for the datapoint. Need to update the server.
        sent_ok = weather_station_server.try_send_data(
            point["SensorType"],
            point["SensorValue"],
            point["ReadingTimeStamp"])

        if sent_ok:
            repository.set_as_sent(point, now)
            sent_count += 1
        else:
            error_count += 1

    print("Sent {} data points. Failed {} data points.".format(sent_count, error_count))


def collect_and_save_sensor_data(repository, sensor):
    repository.save_data_point("Temperature", sensor.get_temperature())
    repository.save_data_point("Humidity", sensor.get_humidity())
    repository.save_data_point("Pressure", sensor.get_pressure())


main()
