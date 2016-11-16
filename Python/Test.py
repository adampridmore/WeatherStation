def main():
    value = 123.456
    print(value)
    print('{}'.format(int(value)))

    method_name("a","b","c")


def method_name(a,b,c):
    print('{0:50} {1:10} {2:10}'.format(a,b,c))


main()
