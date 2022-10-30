n = int(input())

half = n // 2
if n % 2 == 0:
    half -= 1

summation = (half * (half + 1))

if n % 2 == 0:
    summation += n / 2

result = summation / (n + 1)
print("%.6f" % result)
