# Copy from Quera

num = int(input()) - 1
words = input().split()
result = ""
for i in range(num, -1, -1):
    result += words[i] + ' '

print(result.strip())
