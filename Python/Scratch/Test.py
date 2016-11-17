def main():
    value = 123.456
    print(value)
    print('{}'.format(int(value)))

    method_name("a", "b", "c")


def method_name(a, b, c):
    print('{0:50} {1:10} {2:10}'.format(a, b, c))


class MyClass:
    i = 0

    def inc(self):
        self.i += 1

    def get_value(self):
        return self.i

# main()

x = MyClass()
x.inc()
x.inc()
x.inc()
print(x.get_value())
