x1, y1, x2, y2 = map(int, input().split())

if x1 == x2:
    print("Vertical")
elif y1 == y2:
    print("Horizontal")
else:
    print("Try again")
