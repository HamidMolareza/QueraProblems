def calculator(n, m, li):
    # Partition the list into chunks of size 'm' and sum each chunk
    chunk_sums = [sum(li[i:i + m]) for i in range(0, len(li), m)]

    alternating_sum = 0
    for i in range(len(chunk_sums)):
        sign = 1 if i % 2 == 0 else -1
        alternating_sum += (chunk_sums[i] * sign)
    return alternating_sum
