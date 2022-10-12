from typing import List


def get_inputs(num_of_lines: int) -> List[str]:
    result = []
    for i in range(num_of_lines):
        result.append(input())
    return result


def main():
    # Get inputs
    num_of_lines, word_length = map(int, input().split())
    words = get_inputs(num_of_lines)
    search = input()

    counter = 0
    for row in range(num_of_lines):
        for column in range(word_length - len(search) + 1):
            if words[row].find(search, column, column + len(search)) >= 0:
                counter += 1

    if num_of_lines >= len(search):
        for column in range(word_length):
            for row_start_index in range(num_of_lines - len(search) + 1):
                found = True
                for i in range(len(search)):
                    if words[row_start_index + i][column] != search[i]:
                        found = False
                        break
                if found:
                    counter += 1

    print(counter)


main()
