# https://quera.org/problemset/102250/

def find(num1, num2, num3):
    numbers = [1, 2, 3, 4]
    return [number for number in numbers if number not in [num1, num2, num3]][0]
