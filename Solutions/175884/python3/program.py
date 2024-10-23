def calculate_floor(sequence):
    floor = sum(1 if button == 'U' else -1 for button in sequence)
    return floor
