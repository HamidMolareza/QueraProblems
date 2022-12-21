using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using OnRail;
using OnRail.Extensions.OnSuccess;
using OnRail.Extensions.Try;
using OnRail.ResultDetails.Errors;
using Quera.Configs;

namespace Quera.Helpers;

public class Arguments {
    public string ProgramDirectory { get; set; }
    public string OutputDirectory { get; set; }
    public string SolutionsDirectory { get; set; }

    public static Result<Arguments> Get(IReadOnlyCollection<string> args) =>
        GetProgramDirectory(args.FirstOrDefault())
            .OnSuccess(programDirectory => GetOutputDirectory(args.Skip(1).FirstOrDefault())
                .OnSuccess(outputDirectory => GetSolutionsDirectory(args.Skip(2).FirstOrDefault())
                    .OnSuccess(solutionsDirectory => new Arguments {
                        ProgramDirectory = programDirectory,
                        OutputDirectory = outputDirectory,
                        SolutionsDirectory = solutionsDirectory
                    })));

    public static Result<string> GetProgramDirectory(string? programDirectory) =>
        TryExtensions.Try(() => {
            if (string.IsNullOrEmpty(programDirectory)) {
                Console.Write($"Program Directory (contains {ConfigsModel.DataDirectory} directory): ");
                programDirectory = Console.ReadLine();
            }

            if (string.IsNullOrEmpty(programDirectory)
                || !Directory.Exists(Path.Combine(programDirectory, ConfigsModel.DataDirectory))) {
                return Result<string>.Fail(
                    new BadRequestError(message: $"Program directory is not valid. (Input: {programDirectory})"));
            }

            return Result<string>.Ok(programDirectory);
        });

    public static Result<string> GetOutputDirectory(string? outputDirectory) =>
        TryExtensions.Try(() => {
            if (string.IsNullOrEmpty(outputDirectory)) {
                Console.Write($"Output Directory: ");
                outputDirectory = Console.ReadLine();
            }

            if (string.IsNullOrEmpty(outputDirectory) || !Directory.Exists(outputDirectory)) {
                return Result<string>.Fail(
                    new BadRequestError(message: $"Output directory is not valid. (Input: {outputDirectory})"));
            }

            return Result<string>.Ok(outputDirectory);
        });

    public static Result<string> GetSolutionsDirectory(string? solutionsDirectory) =>
        TryExtensions.Try(() => {
            if (string.IsNullOrEmpty(solutionsDirectory)) {
                Console.Write($"Solutions Directory: ");
                solutionsDirectory = Console.ReadLine();
            }

            if (string.IsNullOrEmpty(solutionsDirectory) || !Directory.Exists(solutionsDirectory)) {
                return Result<string>.Fail(
                    new BadRequestError(
                        message: $"Solutions directory is not valid. (Input: {solutionsDirectory})"));
            }

            return Result<string>.Ok(solutionsDirectory);
        });
}