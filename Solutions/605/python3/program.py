# Copy from Quera

n = int(input())
if n <= 2:
    print(n)
else:
    temp = [1, 2]
    for x in range(n - 2):
        temp.append((temp[-1] + temp[-2]) % (10 ** 9 + 7))
    print(temp[-1])
