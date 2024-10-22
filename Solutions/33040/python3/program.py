import threading

import functions


def run(begin, end, func_list):
    threads = []
    for i in range(begin, end):
        thread = threading.Thread(target=func_list[i])
        thread.name = i + 1
        thread.start()
        threads.append(thread)

    for thread in threads:
        thread.join()


def solve():
    f_threads = [
        run(0, 2, functions.f),  # f1, f2
        run(2, 4, functions.f)  # f3, f4
    ]
    run(0, 2, f_threads)  # f1, f2, f3, f4

    run(0, 2, functions.g)  # g1, g2

    run(0, 1, functions.h)  # h1
