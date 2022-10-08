num_of_rows, _ = map(int, input().split())

num_of_stars = 0
for i in range(num_of_rows):
    num_of_stars += input().count('*')

print(num_of_stars)
