def factorial(number: int) -> int:
    if number < 2:
        return 1
    result = 1
    for i in range(2, number + 1):
        result *= i
    return result


n = int(input())
print(factorial(n))
