from typing import List


def get_inputs(num_of_lines: int) -> List[str]:
    result = []
    for i in range(num_of_lines):
        result.append(input())
    return result


# Get inputs
NUM_OF_INPUTS = 5
inputs = get_inputs(NUM_OF_INPUTS)

# Find indexes
indexes = [(index + 1) for index, string in enumerate(inputs) if 'FBI' in string]

# Print
if len(indexes) > 0:
    print(' '.join(map(str, indexes)))
else:
    print("HE GOT AWAY!")
