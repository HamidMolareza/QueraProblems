def sum_digits(num: str) -> str:
    summation = 0
    for digit in num:
        summation += ord(digit) - ord('0')
    return str(summation)


number = input()

while len(number) > 1:
    number = sum_digits(number)

print(number)
