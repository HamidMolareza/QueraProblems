def make_human_readable(volume: int) -> str:
    if volume < 1024:
        return f"{volume}B"
    if volume < 1024 * 1024:
        number = volume // 1024  # Floor division
        return f"{number}KiB"
    number = volume // (1024 * 1024)
    return f"{number}MiB"


def main():
    num_of_inputs = int(input())
    for i in range(num_of_inputs):
        volume = int(input())
        result = make_human_readable(volume)
        print(result)


if __name__ == '__main__':
    main()
