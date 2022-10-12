def main():
    circle_str = input()
    target = input()

    for start_index in range(len(circle_str)):
        found = True
        for i in range(len(target)):
            if target[i] != circle_str[(start_index + i) % len(circle_str)]:
                found = False
                break
        if found:
            print("Yes")
            return

    print("No")


main()
