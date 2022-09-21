def is_dangerous(health_label: str) -> bool:
    num_of_reds = health_label.count('R')
    if num_of_reds >= 3:
        return True

    num_of_yellow = health_label.count('Y')
    if num_of_reds >= 2 and num_of_yellow >= 2:
        return True

    num_of_greens = health_label.count('G')
    if num_of_greens < 1:
        return True
    return False


if is_dangerous(input()):
    print("nakhor lite")
else:
    print("rahat baash")
