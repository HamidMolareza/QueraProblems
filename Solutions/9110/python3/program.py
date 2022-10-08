def inverse(string: str) -> str:
    result = ""
    for c in string:
        if c == "1":
            result += "0"
        else:
            result += "1"
    return result


def generate_sequence(max_length: int) -> str:
    result = "1"
    while len(result) < max_length:
        result += inverse(result)
    return result


begin, end = map(int, input().split())
sequence = generate_sequence(end)

print(sequence[begin - 1: end])
