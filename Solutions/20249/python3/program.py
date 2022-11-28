import math

num_of_jars, capacity = map(int, input().split())
jams = list(map(int, input().split()))

sum_of_jams = sum(jams)
remain_jars = num_of_jars - (math.ceil(sum_of_jams / capacity))

print(remain_jars)
