
n = 0
def swap (a ,b):
    c = a
    a = b
    b = c
    
    return [a, b]

def print_matrix (table):
    for i in table:
        for j in range (n):
            print(i[j], end="")
        
        print ()
    

def spl (s : str):
    res = []
    for i in s:
        res.append(i)
        
    return res

def horiz (table : list):
    
    for i in range (n // 2):
        for j in range (n):

            table[i][j] , table[n - (i + 1)][j] = swap(table[i][j] , table[n - (i + 1)][j])

            
    
    return table

def vert (table : list):
    
    for i in range (n):
        for j in range (n // 2):


            table[i][j] ,table[i][n - (j + 1)] = swap(table[i][j] ,table[i][n - (j + 1)])
            
    
    return table

def ni (A : list):
    N = len(A[0])
    for i in range(N // 2):
        for j in range(i, N - i - 1):
            temp = A[i][j]
            A[i][j] = A[N - 1 - j][i]
            A[N - 1 - j][i] = A[N - 1 - i][N - 1 - j]
            A[N - 1 - i][N - 1 - j] = A[j][N - 1 - i]
            A[j][N - 1 - i] = temp
    
    return A


n = int(input())
table = []
commands = []

for i in range (n):
    table.append(spl(input()))
    
commands_count = int(input())

for i in range (commands_count):
    commands.append(input())
    
for i in commands:
    if i == "H":
        table = horiz(table)
        
    if i == "V":
        table = vert(table)
        
    if i == "90":
        table = ni(table)
        
print_matrix(table)