n = int(input())

count = 0
while n >= 2:
    count += 1
    n /= 2

print(2 ** (count + 1))
