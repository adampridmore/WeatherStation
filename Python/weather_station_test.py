import SensorDataRepository
import datetime


def main():
    repository = SensorDataRepository.SensorDataRepository()

    # repository.delete_all()

    repository.save_data_point("TestSensor", 123)
    repository.save_data_point("TestSensor", 456)
    repository.save_data_point("TestSensor", 789)

    unsent_data = repository.get_unsent_data()

    now = datetime.datetime.now()
    for point in unsent_data:
        #     send data here
        # print(point)
        repository.set_as_sent(point, now)

    repository.print_all_rows()


main()
