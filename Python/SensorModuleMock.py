class SensorModule:
    def get_temperature(self):
        return 10.123

    def get_humidity(self):
        return 30.123

    def get_pressure(self):
        return 500.123

    def show_message(self, message):
        print("HAT: " + message)

    def show_verion(self):
        print("*****************************")
        print("**** MOCK HAT SENSOR !!! ****")
        print("*****************************")
