# About this solution

This solution has scored 100 in [Quera](https://quera.org/).

🌟 If you like this solution, please give it a star.

# Usage

## Prerequisites

Considering that Quera uses `V8`, we need to use Docker container to run the program. Docker can be downloaded from the official website: https://www.docker.com/get-started

## Run

We use [hamidmolareza/d8](https://github.com/HamidMolareza/v8-docker) container to run this program.
Use the following command to run:

```shell
docker run --rm -it -v $PWD:/src/ hamidmolareza/d8 run /src/program.js -d /src/inputs
```

For simplicity, you can also use the existing [Makefile](Makefile):

```shell
make run
```

## Support

Reach out to the maintainer at one of the following places:

- [GitHub issues](https://github.com/HamidMolareza/QueraProblems/issues/new?assignees=&labels=question&template=04_SUPPORT_QUESTION.md&title=support%3A+)

## Authors & contributors

The original setup of this repository is by [Hamid Molareza](https://github.com/HamidMolareza).

## License

This solution is licensed under the [GPLv3](https://choosealicense.com/licenses/gpl-3.0/).
