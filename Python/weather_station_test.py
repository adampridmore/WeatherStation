import SensorDataRepository
import datetime
import SensorModule as SensorModule


def main():
    repository = SensorDataRepository.SensorDataRepository()

    # repository.delete_all();

    sensor = SensorModule.SensorModule()

    collect_sensor_data(repository, sensor)

    try_send_data(repository)

    repository.print_all_rows()


def try_send_data(repository):
    unsent_data = repository.get_unsent_data()
    now = datetime.datetime.now()
    for point in unsent_data:
        #     send data here
        # print(point)
        repository.set_as_sent(point, now)


def collect_sensor_data(repository, sensor):
    repository.save_data_point("Temperature", sensor.get_temperature())
    repository.save_data_point("Humidity", sensor.get_humidity())
    repository.save_data_point("Pressure", sensor.get_pressure())


main()
