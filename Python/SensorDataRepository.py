import sqlite3
import datetime


class SensorDataRepository:
    connection = None

    def __init__(self):
        def dict_factory(cursor, row):
            d = {}
            for idx, col in enumerate(cursor.description):
                d[col[0]] = row[idx]
            return d

        self.connection = sqlite3.connect('test.db')

        self.connection.row_factory = dict_factory

    def create_table(self):
        # conn.execute('''DROP TABLE DataPoints''')
        # print("Table dropped")

        self.connection.execute('''CREATE TABLE IF NOT EXISTS DataPoints (
        Id                  INTEGER     PRIMARY KEY AUTOINCREMENT NOT NULL,
        SensorType          TEXT        NOT NULL,
        SensorValue         DOUBLE      NOT NULL,
        ReadingTimeStamp    DATETIME    NOT NULL,
        SentTimeStamp       DATETIME    NULL)''')

    def save_data_point(self, sensor_type, sensor_value):
        now = datetime.datetime.now()

        self.connection.execute("INSERT INTO DataPoints (SensorType, SensorValue, ReadingTimeStamp) \
                VALUES (?, ?, ?)", [sensor_type, sensor_value, now])

        self.connection.commit()

    def get_all_data_points(self):
        return self.connection.execute("SELECT * FROM DataPoints").fetchall()

    def get_unsent_data(self):
        return self.connection.execute("SELECT * FROM DataPoints WHERE SentTimeStamp IS NULL").fetchall()

    def set_as_sent(self, data_point, now):
        self.connection.execute("UPDATE DataPoints SET SentTimeStamp = ? WHERE Id = ?", [now, data_point["Id"]])
        self.connection.commit()

    def delete_all(self):
        self.connection.execute("DELETE FROM DataPoints")

    def print_all_rows(self):
        for row in self.get_all_data_points():
            print(row)
