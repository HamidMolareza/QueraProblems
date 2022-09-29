from typing import List


def get_inputs() -> List[int]:
    result = []
    while True:
        number = int(input())
        if number == 0:
            return result
        result.append(number)


inputs = get_inputs()
inputs.reverse()
print(*inputs, sep='\n')
