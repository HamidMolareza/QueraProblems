# Input the size of the arrays
n = int(input())

# Input the first array of integers
n1 = list(map(int, input().split()))

# Input the second array of integers
n2 = list(map(int, input().split()))

# Use a list comprehension to filter elements based on the condition
m = [n1[i] for i in range(n) if n2[i] == 1]

# Sort the filtered list
m.sort()

# Print the elements separated by a space
print(*m)
