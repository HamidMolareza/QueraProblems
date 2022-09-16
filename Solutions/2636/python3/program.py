input_pieces = list(map(int, input().split()))

valid_pieces = [1, 1, 2, 2, 2, 8]

for i in range(len(valid_pieces)):
    print(valid_pieces[i] - input_pieces[i], end=" ")