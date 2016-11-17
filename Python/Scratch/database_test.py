import sqlite3

conn = sqlite3.connect('test.db')

print("Opened database successfully")

conn.execute('''DROP TABLE COMPANY''')

print("Table dropped")

conn.execute('''CREATE TABLE COMPANY
       (ID INT PRIMARY KEY     NOT NULL,
       NAME           TEXT    NOT NULL,
       AGE            INT     NOT NULL,
       ADDRESS        CHAR(50),
       SALARY         REAL);''')

print("Table created successfully")

conn.execute("INSERT INTO COMPANY (ID,NAME,AGE,ADDRESS,SALARY) \
      VALUES (1, 'Paul', 32, 'California', 20000.00 )")

conn.execute("INSERT INTO COMPANY (ID,NAME,AGE,ADDRESS,SALARY) \
      VALUES (2, 'Dave', 40, 'New York', 21000.00 )")

data = conn.execute("SELECT * FROM COMPANY").fetchall()

print(data)

