import SensorDataRepository
import datetime


def main():
    repository = SensorDataRepository.SensorDataRepository()

    # repository.create_tables()

    repository.flag_all_as_unsent()

    # for point in repository.get_all_data_points():
    #     print(point)


    # for point in repository.get_unsent_data():
    #     print(point)
    #     repository.set_as_sent(point, datetime.datetime.now())
    #

    repository.print_all_rows()
    print(repository.get_data_stats())


main()
