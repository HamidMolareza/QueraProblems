def quick_sort(arr):
    stack = [(0, len(arr) - 1)] 

    while stack:
        start, end = stack.pop()
        if start >= end:
            continue

        pivot = arr[start] 
        left = start + 1
        right = end

        while left <= right:
            while left <= right and arr[left] < pivot:
                left += 1
            while left <= right and arr[right] >= pivot:
                right -= 1
            if left < right:
                arr[left], arr[right] = arr[right], arr[left]

        arr[start], arr[right] = arr[right], arr[start] 

        stack.append((start, right - 1))
        stack.append((right + 1, end))

    return arr

numbers = input().split()
numbers = [int(num) for num in numbers]

sorted_numbers = quick_sort(numbers)
print(" ".join(map(str, sorted_numbers)))
