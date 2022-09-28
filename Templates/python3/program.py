from typing import List


def get_inputs(num_of_lines: int) -> List[str]:
    result = []
    for i in range(num_of_lines):
        result.append(input())
    return result


# Sample
num_of_inputs = 4
inputs = list(map(float, get_inputs(num_of_inputs)))

# Sample
inputs = [int(x) for x in input().split()]
