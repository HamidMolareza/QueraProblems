weight = int(input())
height = float(input())

bmi = weight / (height * height)
print("%.2f" % bmi)

if bmi < 18.5:
    print("Underweight")
elif bmi < 25:
    print("Normal")
elif bmi < 30:
    print("Overweight")
else:
    print("Obese")
