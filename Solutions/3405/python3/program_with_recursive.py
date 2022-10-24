def print_recursive():
    digit = input()
    if digit != "0":
        print_recursive()
        print(digit)


print_recursive()
