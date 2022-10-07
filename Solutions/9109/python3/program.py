input()  # ignore
markers = sorted([int(i) for i in input().split()])

group: dict = {}
for marker in markers:
    value = group.get(marker)
    if value is None:
        group[marker] = 1
    else:
        group.update({marker: int(value) + 1})

sorted_base_count = sorted(group.items(), key=lambda item: item[1])
print(sorted_base_count[0][0])

