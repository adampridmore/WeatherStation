import SensorDataRepository
import datetime


def main():
    repository = SensorDataRepository.SensorDataRepository()

    repository.create_tables()

    # repository.delete_all()
    # repository.flag_all_as_unsent()

    repository.print_all_rows()
    print(repository.get_data_stats())


main()
