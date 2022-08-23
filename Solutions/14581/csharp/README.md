# About this solution

This solution has scored 100 in [Quera](https://quera.org/).

🌟 If you like this solution, please give it a star.

## Solution

1. If n is **odd** like 9, the sequence is as follows:

`0 1 2 3 4 4 3 2 1 0`

The sum of this sequence:

`(0 + 1 + 2 + 3 + 4)` + `(0 + 1 + 2 + 3 + 4)` = `2 * ((0 + 1 + 2 + 3 + 4))`

It means twice the sum of the numbers 1 to int(9/2)

2. If n is **even**, like 10, the sequence is as follows:

`0 1 2 3 4 5 4 3 2 1 0`

The sum of this sequence:

`(0 + 1 + 2 + 3 + 4)` + `(4 + 3 + 2 + 1 + 0)` + `5` = `2 * (0 + 1 + 2 +3 + 4)` + `5`

That is, double the sum of the numbers 1 to `(n/2)-1` and then sum it with `n/2`.

3. To add the numbers 1 to `n` we have:

`(n * (n + 1))/2`

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