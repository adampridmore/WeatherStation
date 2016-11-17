from sense_hat import SenseHat
import time

sense = SenseHat()

X = [255, 0, 255]  # purple
O = [50, 50, 50]  # gray

image1 = [
    O, O, X, X, X, X, O, O,
    O, X, O, O, O, O, X, O,
    X, O, X, O, X, O, O, X,
    X, O, X, O, X, O, O, X,
    X, O, O, O, O, X, O, X,
    X, O, X, X, X, O, O, X,
    O, X, O, O, O, O, X, O,
    O, O, X, X, X, X, O, O,
]

while (True):
    time.sleep(1)
    sense.set_pixels(image1)
