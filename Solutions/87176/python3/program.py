def game(number):
    string = sorted(str(number))
    return int(string[1]) - int(string[0])
