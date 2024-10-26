print(' '.join(sorted([c if (ord(c) - 97) % 2 == 0 else c.upper() for c in input()], reverse=True)))
