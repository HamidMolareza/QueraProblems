def diff(input1: int, input2: int, input3: int) -> int:
    if input1 == input2:
        return input3
    if input1 == input3:
        return input2
    if input2 == input3:
        return input1


x1, y1 = map(int, input().split())
x2, y2 = map(int, input().split())
x3, y3 = map(int, input().split())

x_target = diff(x1, x2, x3)
y_target = diff(y1, y2, y3)

print(x_target, y_target)
