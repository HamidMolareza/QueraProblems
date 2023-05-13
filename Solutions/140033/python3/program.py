string = input()

vowel_count = sum(1 for char in string if char in "aeiou")
print(vowel_count)
