time, waitAfterArAr, waitAfterMaMa = map(int, input().split())

cycle = waitAfterArAr + waitAfterMaMa + 2
minimumCycle = int(time / cycle)

numOfArr, numOfMaMa = minimumCycle, minimumCycle

remain = time % cycle
if remain > 0:
    numOfArr += 1
if remain - 1 - waitAfterArAr > 0:
    numOfMaMa += 1

print(numOfArr, numOfMaMa)
