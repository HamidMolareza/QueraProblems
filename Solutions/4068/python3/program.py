n, k = map(int, input().split())

if k == 1:
    for i in range(1, n + 1):
        print(i, end=' ')
elif k > n / 2:
    print("Impossible")
else:
    even = (n // 2) + 1
    odd = 1

    for i in range(0, n // 2):
        print(even, odd, end=' ')
        even += 1
        odd += 1
    if n % 2 != 0:
        print(n)
