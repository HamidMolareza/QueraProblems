n = int(input())

for row in range(1, n + 1):
    for column in range(1, n + 1):
        print(row * column, end=" ")
    print()
