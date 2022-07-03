# QueraProblems
The answer to some [Quera](https://quera.org) problems in C#.

For more information please checkout to [master branch](https://github.com/HamidMolareza/QueraProblems).

## Solution
The numbers are 10<sup>x</sup> which 0<=x<=100.

### Multiplication
10<sup>x</sup> * 10<sup>y</sup> = 10<sup>x + y</sup>

### Sumation
10000 + 100 = 10100

For 2 numbers we have only 2 non-zero value Therefore, it is enough to know to which index we must add the non-zero number (from smaller number to bigger number). for example:

Bigger number: 10000 (length: 5)

Smaller number: 100 (length: 3)

we must add 1 to index 2 (5 - (3 - 0))
