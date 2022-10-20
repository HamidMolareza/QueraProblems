def find_nearest_station(k: int, position: int) -> int:
    remain = abs(position) % k
    if remain == 0:
        return position
    if abs(position) % k > k / 2:
        return k * round(position / k)
    return k * int(position / k)


def main():
    k, person1, person2 = map(int, input().split())

    only_train1 = abs(person1 - person2)

    person1_nearest_station = find_nearest_station(k, person1)
    train1_and_train2 = abs(person1_nearest_station - person1)

    person2_nearest_station = find_nearest_station(k, person2)
    train1_and_train2 += abs(person2_nearest_station - person2)

    remain_distance = abs(person2_nearest_station - person1_nearest_station)
    train1_and_train2 += remain_distance // k

    print(min(train1_and_train2, only_train1))


if __name__ == '__main__':
    main()
