# History

## Version 1

In version 1 of the program, we kept each solution in a **separate branch**. Because we just wanted to put `C#` language
solutions. In this way, we could create a new branch from the base branch (`Utility`) to solve each question, which had
the base methods, and start solving the problem very quickly. But this method has its advantages and disadvantages.
Including:

- The number of branches becoming too large
- For others, the structure was not very understandable
- It was not easy to support multiple languages
- Access to any solution required checkout the branch
- and so on

You can see the **old README file** from [this link](old-readme.md).

## Version 2

In version 2 of the program, we decided to keep all the solutions in the `master` branch. In the folder
named `Solutions`. For this, it was necessary to checkout the branch of each solution and then copy the solutions. For
this reason, we wrote the shell code `move.sh`.

You can see this shell code from [this link](MoveBranches/move.sh).