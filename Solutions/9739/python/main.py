def calculate_largest_rectangle_area(heights):
    stack = []
    max_area = 0
    i = 0

    while i < len(heights):
        if not stack or heights[stack[-1]] <= heights[i]:
            stack.append(i)
            i += 1
        else:
            top = stack.pop()
            area_with_top = heights[top] * (i if not stack else i - stack[-1] - 1)

            if max_area < area_with_top:
                max_area = area_with_top

    while stack:
        top = stack.pop()
        area_with_top = heights[top] * (i if not stack else i - stack[-1] - 1)

        if max_area < area_with_top:
            max_area = area_with_top

    return max_area


if __name__ == "__main__":
    n = int(input())
    heights = list(map(int, input().split()))

    result = calculate_largest_rectangle_area(heights)
    print(result)
