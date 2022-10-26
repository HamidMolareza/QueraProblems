import math
from typing import List


def get_extra_waters(pots: List[int], water_in_each_pots: float) -> float:
    extra_waters = 0.0
    for pot in pots:
        if pot > water_in_each_pots:
            extra_waters += pot - water_in_each_pots
    return extra_waters


def calculate_minimum_steps(pots: List[int]) -> int:
    water_in_each_pots = sum(pots) * 1.0 / len(pots)
    extra_waters = get_extra_waters(pots, water_in_each_pots)
    if water_in_each_pots == 0:
        return 0
    return math.ceil(extra_waters / water_in_each_pots)


def main():
    pots = list(map(int, input().split()))
    minimum_steps = calculate_minimum_steps(pots)
    print(minimum_steps)


if __name__ == '__main__':
    main()
