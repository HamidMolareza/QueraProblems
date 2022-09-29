length = int(input())
main_str = input()
user_str = input()

num_of_wrongs = 0
for i in range(length):
    if main_str[i] != user_str[i]:
        num_of_wrongs += 1

print(num_of_wrongs)
