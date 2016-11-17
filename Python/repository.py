import sqlite3
import datetime


def create_tables(conn):
    # conn.execute('''DROP TABLE DataPoints''')
    # print("Table dropped")

    conn.execute('''CREATE TABLE IF NOT EXISTS DataPoints (
        Id                  INTEGER     PRIMARY KEY AUTOINCREMENT NOT NULL,
        SensorType          TEXT        NOT NULL,
        SensorValue         DOUBLE      NOT NULL,
        ReadingTimeStamp    DATETIME    NOT NULL,
        SentTimeStamp       DATETIME    NULL)''')


def save_data_point(conn, sensor_type, sensor_value):
    now = datetime.datetime.now()

    conn.execute("INSERT INTO DataPoints (SensorType, SensorValue, ReadingTimeStamp) \
      VALUES (?, ?, ?)", [sensor_type, sensor_value, now])

    conn.commit()


def connect():
    conn = sqlite3.connect('test.db')
    print("Opened database successfully")

    return conn
