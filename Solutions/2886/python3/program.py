hour, minute = map(int, input().split())

real_hour = (12 - hour) % 12
real_minute = (60 - minute) % 60

print("%02d:%02d" % (real_hour, real_minute))
