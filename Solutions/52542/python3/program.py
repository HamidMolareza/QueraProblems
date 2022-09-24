input()  # ignore
groups = map(int, input().split())

for group in groups:
    if group <= 3:
        print("*" * group)
    else:
        print("*")
