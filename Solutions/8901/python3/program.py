num_of_movements, position = input().split()

for i in range(int(num_of_movements)):
    movement = input().split()
    if movement[0] == position:
        position = movement[1]
    elif movement[1] == position:
        position = movement[0]

print(position)
