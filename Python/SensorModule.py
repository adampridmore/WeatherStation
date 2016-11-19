from sense_hat import SenseHat


class SensorModule:
    def __init__(self):
        self.sense = SenseHat()

    def get_temperature(self):
        return self.sense.get_temperature()

    def get_humidity(self):
        return self.sense.get_humidity()

    def get_pressure(self):
        return self.sense.get_pressure()

    def show_message(self, message):
        self.sense.show_message(message)

    def show_verion(self):
        print("real HAT sensor")
