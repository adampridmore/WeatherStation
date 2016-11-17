import SensorDataRepository
import datetime
import time
import SensorModule as SensorModule
import WeatherStationServer


def main():
    repository = SensorDataRepository.SensorDataRepository()
    sensor = SensorModule.SensorModule()
    weatherStationServer = WeatherStationServer.WeatherStationServer()

    repository.create_tables()

    # repository.delete_all();

    while True:
        collect_and_send_data(repository, sensor, weatherStationServer)


def collect_and_send_data(repository, sensor, weatherStationServer):
    collect_and_save_sensor_data(repository, sensor)

    try_send_data(repository, weatherStationServer)

    repository.print_all_rows()
    print(repository.get_data_stats())

    time.sleep(30)


def try_send_data(repository, weatherStationServer):
    unsent_data = repository.get_unsent_data()
    now = datetime.datetime.now()

    for point in unsent_data:
        # TODO - Send the client's timestamp for the datapoint. Need to update the server.
        weatherStationServer.send_data(point["SensorType"], point["SensorValue"])
        repository.set_as_sent(point, now)


def collect_and_save_sensor_data(repository, sensor):
    repository.save_data_point("Temperature", sensor.get_temperature())
    repository.save_data_point("Humidity", sensor.get_humidity())
    repository.save_data_point("Pressure", sensor.get_pressure())


main()
