def is_correct(person_pattern: str, index: int, key: str):
    if key == person_pattern[index % len(person_pattern)]:
        return True
    return False


persons = [
    {"name": "keyvoon", "pattern": "331122", "score": 0},
    {"name": "nezam", "pattern": "123", "score": 0},
    {"name": "shir farhad", "pattern": "2123", "score": 0}
]

_ = input()
keys = input()

# Calculate scores and max score:
max_score = 0
for i in range(len(keys)):
    for person in persons:
        if is_correct(person["pattern"], i, keys[i]):
            person["score"] += 1
            if person["score"] > max_score:
                max_score = person["score"]

# Select top persons:
top_persons = []
for person in persons:
    if person["score"] == max_score:
        top_persons.append(person)

# Select names base name
sorted_persons = sorted(top_persons, key=lambda d: d['name'])
names = [sub['name'] for sub in sorted_persons]

# Result
print(max_score)
print(*names, sep="\n")
