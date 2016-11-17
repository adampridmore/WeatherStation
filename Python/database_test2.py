import repository


def main():
    conn = repository.connect()

    repository.create_tables(conn)

    sensor_type = "TestSensor"
    sensor_value = 456

    repository.save_data_point(conn, sensor_type, sensor_value)

    data = conn.execute("SELECT * FROM DataPoints").fetchall()
    print(data)


main()
