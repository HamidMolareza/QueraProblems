from typing import List


def get_inputs(num_of_lines: int) -> List[str]:
    result = []
    for i in range(num_of_lines):
        result.append(input())
    return result


num_of_inputs = 2
num1, num2 = get_inputs(num_of_inputs)

num1_reversed = num1[::-1]
num2_reversed = num2[::-1]

if num1_reversed > num2_reversed:
    num1, num2 = num2, num1

print(num1, end="")
if num1 == num2:
    print(" = ", end="")
else:
    print(" < ", end="")
print(num2)
