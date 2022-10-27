n, k = map(int, input().split())

next_person = 1
count = 0
while True:
    count += 1
    next_person = (next_person + k) % n
    if next_person == 1:
        break
    if next_person == 0:
        next_person = n

print(count)
