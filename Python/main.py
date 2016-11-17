import SensorDataRepository
import datetime
import time
import SensorModuleMock as SensorModule
import WeatherStationServer

defaultPollTime = 5
# weatherStationServerUrl = 'http://calf/WeatherStationServer/api/dataPoints'
# weatherStationServerUrl = 'http://localhost/WeatherStationServer/api/dataPoints'
weatherStationServerUrl = 'http://localhost:59653/api/dataPoints'


def main():
    repository = SensorDataRepository.SensorDataRepository()
    sensor = SensorModule.SensorModule()
    weather_station_server = WeatherStationServer.WeatherStationServer(weatherStationServerUrl)

    repository.create_tables()

    # repository.delete_all();

    while True:
        temp = sensor.get_temperature()
        sensor.show_message("{}c".format(round(temp,1)))

        collect_and_send_data(repository, sensor, weather_station_server)


def collect_and_send_data(repository, sensor, weather_station_server):
    collect_and_save_sensor_data(repository, sensor)

    try_send_data(repository, weather_station_server)

    # repository.print_all_rows()
    print(repository.get_data_stats())

    time.sleep(defaultPollTime)


def try_send_data(repository, weather_station_server):
    unsent_data = repository.get_unsent_data()
    now = datetime.datetime.now()

    sent_count = 0
    error_count = 0
    for point in unsent_data:
        # TODO - Send the client's timestamp for the datapoint. Need to update the server.
        if weather_station_server.send_data(point["SensorType"], point["SensorValue"]):
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
