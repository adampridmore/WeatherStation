import SensorDataRepository
import datetime
import time
import SensorModule as SensorModule
import WeatherStationServer


def main():
    repository = SensorDataRepository.SensorDataRepository()
    sensor = SensorModule.SensorModule()
    weather_station_server = WeatherStationServer.WeatherStationServer()

    repository.create_tables()

    # repository.delete_all();

    while True:
        collect_and_send_data(repository, sensor, weather_station_server)


def collect_and_send_data(repository, sensor, weather_station_server):
    collect_and_save_sensor_data(repository, sensor)

    try_send_data(repository, weather_station_server)

    # repository.print_all_rows()
    print(repository.get_data_stats())

    time.sleep(5)


def try_send_data(repository, weather_station_server):
    unsent_data = repository.get_unsent_data()
    now = datetime.datetime.now()

    sent_count = 0
    for point in unsent_data:
        # TODO - Send the client's timestamp for the datapoint. Need to update the server.
        weather_station_server.send_data(point["SensorType"], point["SensorValue"])
        repository.set_as_sent(point, now)
        sent_count += 1

    print("Sent {} data points ".format(sent_count))


def collect_and_save_sensor_data(repository, sensor):
    repository.save_data_point("Temperature", sensor.get_temperature())
    repository.save_data_point("Humidity", sensor.get_humidity())
    repository.save_data_point("Pressure", sensor.get_pressure())


main()
