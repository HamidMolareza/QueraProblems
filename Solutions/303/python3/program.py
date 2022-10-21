def show_fib_nth(m: int, n: int):
    if m <= 0:
        return
    print(m)
    show_fib_nth(n - m, m)


m = int(input())
n = int(input())

show_fib_nth(m, n)
