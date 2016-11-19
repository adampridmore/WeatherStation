import sqlite3
import datetime


class SensorDataRepository:
    def __init__(self):
        def dict_factory(cursor, row):
            d = {}
            for idx, col in enumerate(cursor.description):
                d[col[0]] = row[idx]
            return d

        self.connection = sqlite3.connect('test.db')

        self.connection.row_factory = dict_factory

    def create_tables(self):
        # conn.execute('''DROP TABLE DataPoints''')
        # print("Table dropped")

        self.connection.execute('''CREATE TABLE IF NOT EXISTS DataPoints (
        Id                  INTEGER     PRIMARY KEY AUTOINCREMENT NOT NULL,
        SensorType          TEXT        NOT NULL,
        SensorValue         DOUBLE      NOT NULL,
        ReadingTimeStamp    DATETIME    NOT NULL,
        SentTimeStamp       DATETIME    NULL)''')

        self.connection.execute('''CREATE INDEX IF NOT EXISTS SentTimeStamp ON DataPoints (SentTimeStamp);''')

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
        self.connection.commit()

    def print_all_rows(self):
        for row in self.get_all_data_points():
            print(row)

    def get_data_stats(self):
        sent = self.connection.execute("SELECT COUNT(*) As Count\
                                        FROM DataPoints \
                                        WHERE SentTimeStamp IS NOT NULL").fetchone()["Count"]
        to_send = self.connection.execute(" SELECT COUNT(*) As Count \
                                            FROM DataPoints \
                                            WHERE SentTimeStamp IS NULL").fetchone()["Count"]

        return {
            "now": datetime.datetime.now().isoformat(),
            "sent": sent,
            "to_send": to_send,
            "total": (sent + to_send)
        }

    def flag_all_as_unsent(self):
        self.connection.execute("UPDATE DataPoints SET SentTimeStamp = NULL")

        self.connection.commit()
