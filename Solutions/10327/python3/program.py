def main():
    num_of_codes, valid_code = input().split()
    valid_chars = set(valid_code)

    for i in range(int(num_of_codes)):
        code_chars = set(input())
        is_valid = code_chars == valid_chars
        if is_valid:
            print("Yes")
        else:
            print("No")


if __name__ == '__main__':
    main()
