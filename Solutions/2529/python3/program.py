num_of_rows = int(input())

num_of_chars = []
for i in range(num_of_rows):
    num_of_chars.append(len(set(input())))

print(max(num_of_chars))
