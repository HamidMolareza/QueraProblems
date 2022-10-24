from typing import List


def get_inputs(num_of_lines: int) -> List[str]:
    result = []
    for i in range(num_of_lines):
        result.append(input())
    return result


# Sample
num_of_coin_columns = int(input())
coin_columns = list(map(int, get_inputs(num_of_coin_columns)))

average = int(sum(coin_columns) / num_of_coin_columns)

minutes = 0
for coin_column in coin_columns:
    if coin_column >= average:
        continue
    minutes += average - coin_column

print(minutes)
