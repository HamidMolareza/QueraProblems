while True:
    try:
        number = int(input())
        square = number ** 2
        print(str(square)[::-1])
    except:
        break
