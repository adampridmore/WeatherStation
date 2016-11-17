from sense_hat import SenseHat


class SensorModule:
    sence = None

    def __init__(self):
        sense = SenseHat()

    def get_temperature(self):
        self.sence.get_temperature()

    def get_humidity(self):
        self.sence.get_humidity()

    def get_pressure(self):
        self.sence.get_pressure()

    def show_message(self, message):
        self.sence.show_message(message)