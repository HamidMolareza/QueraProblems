from typing import List


def get_inputs(num_of_lines: int) -> List[str]:
    result = []
    for i in range(num_of_lines):
        result.append(input())
    return result


def multiply(input_list: List[float]) -> float:
    result = 1
    for number in input_list:
        result *= number
    return result


num_of_inputs = 4
inputs = list(map(float, get_inputs(num_of_inputs)))

summation = sum(inputs)
average = summation / num_of_inputs
product = multiply(inputs)
maximum = max(inputs)
minimum = min(inputs)

print('Sum : %.6f' % summation)
print('Average : %.6f' % average)
print('Product : %.6f' % product)
print('MAX : %.6f' % maximum)
print('MIN : %.6f' % minimum)
