# About this solution

This solution has scored 100 in [Quera](https://quera.org/).

🌟 If you like this solution, please give it a star.

## Solution

The numbers are 10<sup>x</sup> which 0<=x<=100.

### Multiplication

10<sup>x</sup> * 10<sup>y</sup> = 10<sup>x + y</sup>

### Sumation

10000 + 100 = 10100

For 2 numbers we have only 2 non-zero value Therefore, it is enough to know to which index we must add the non-zero
number (from smaller number to bigger number). for example:

Bigger number: 10000 (length: 5)

Smaller number: 100 (length: 3)

we must add 1 to index 2 (5 - (3 - 0))

## Built With

C# .NET 5

## Usage

1. In the directory, run `dotnet build`
2. `dotnet run`

## Support

Reach out to the maintainer at one of the following places:

- [GitHub issues](https://github.com/HamidMolareza/QueraProblems/issues/new?assignees=&labels=question&template=04_SUPPORT_QUESTION.md&title=support%3A+)

## Authors & contributors

The original setup of this repository is by [Hamid Molareza](https://github.com/HamidMolareza).

## License

This solution is licensed under the [GPLv3](https://choosealicense.com/licenses/gpl-3.0/).