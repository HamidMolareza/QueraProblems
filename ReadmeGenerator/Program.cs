using System;
using System.CommandLine;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnRail.Extensions.OnFail;
using Quera.Cache;
using Quera.Collector;
using Quera.Configs;
using Quera.Generator;
using Quera.Helpers;
using Serilog;

namespace Quera;

public static class Program {
    public static Task Main(string[] args) {
        ConfigSerilog();

        // Setup DI container
        var services = new ServiceCollection();

        var appSettings = services.ConfigAppSettings();
        services.AddTransient<CollectorService>();
        services.AddTransient<GeneratorService>();
        services.AddTransient<CacheRepository>();
        services.AddTransient<AppRunner>(); // Register AppRunner

        Log.Debug("Services are registered.");

        var serviceProvider = services.BuildServiceProvider();

        return InvokeCommandLine(args, appSettings, serviceProvider);
    }

    private static void ConfigSerilog() {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Console(outputTemplate:
                "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
            .CreateLogger();
    }

    private static Task<int>
        InvokeCommandLine(string[] args, AppSettings appSettings, IServiceProvider serviceProvider) {
        // Define the options
        var delayToRequestQueraInMilliSecondsOption = new Option<int>(
            ["-d", "--request-delay"],
            () => appSettings.DelayToRequestQueraInMilliSeconds
        );
        delayToRequestQueraInMilliSecondsOption.AddValidator(result => {
            if (result.GetValueOrDefault<int>() < 1)
                result.ErrorMessage = "The delay value must more than 1";
        });

        var readmeTemplatePathOption = new Option<string>(
            ["-t", "--readme-template"],
            () => appSettings.ReadmeTemplatePath
        );
        readmeTemplatePathOption.AddValidator(result => {
            var value = result.GetValueOrDefault<string>();
            if (value is null || !File.Exists(value))
                result.ErrorMessage = "The readme template path is not valid.";
        });

        var outputOption = new Option<string>(
            ["-o", "--output"],
            () => appSettings.ReadmeOutputPath,
            "The readme output path");

        var workingDirectoryOption = new Option<string>(
            ["-w", "--working-directory"],
            () => appSettings.WorkingDirectory,
            "The working directory of the application");
        workingDirectoryOption.AddValidator(result => {
            if (!Directory.Exists(result.GetValueOrDefault<string>()))
                result.ErrorMessage = "The working directory is not valid.";
        });

        var solutionsOption = new Option<string>(
            ["-s", "--solutions"],
            () => appSettings.SolutionsPath,
            "The solutions directory");
        solutionsOption.AddValidator(result => {
            if (!Directory.Exists(result.GetValueOrDefault<string>()))
                result.ErrorMessage = "The solutions directory is not valid.";
        });

        // Create root command and add options
        var rootCommand = new RootCommand {
            delayToRequestQueraInMilliSecondsOption,
            readmeTemplatePathOption,
            outputOption,
            workingDirectoryOption,
            solutionsOption
        };

        // Set handler for root command
        rootCommand.SetHandler(async (delayToRequestQueraInMilliSeconds, readmeTemplatePath,
                workingDirectory, outputPath, solutionsPath) => {
                // Get AppSettings instance from DI
                var settings = serviceProvider.GetService<AppSettings>();
                if (settings is null) throw new Exception("Can not get app settings from DI.");

                settings.DelayToRequestQueraInMilliSeconds = delayToRequestQueraInMilliSeconds;
                settings.ReadmeTemplatePath = readmeTemplatePath;
                settings.WorkingDirectory = workingDirectory;
                settings.ReadmeOutputPath = outputPath;
                settings.SolutionsPath = solutionsPath;

                // Call other classes/methods with DI
                var runner = serviceProvider.GetService<AppRunner>();
                if (runner is null) throw new Exception("Can not get app runner from DI.");

                await runner.RunAsync()
                    .OnFailTee(result => {
                        //for GitHub action: https://github.com/HamidMolareza/QueraProblems/issues/10
                        Environment.ExitCode = -1;

                        Log.Error("The operation failed. See the below text for more information:\n{result}",
                            result.ToStr());
                    });
            }, delayToRequestQueraInMilliSecondsOption, readmeTemplatePathOption, workingDirectoryOption, outputOption,
            solutionsOption);

        return rootCommand.InvokeAsync(args);
    }

    private static AppSettings ConfigAppSettings(this IServiceCollection services) {
        // Build Configuration from appsettings.json
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        // Bind AppSettings from Configuration
        var appSettings = configuration.Get<AppSettings>() ?? new AppSettings();
        services.AddSingleton(appSettings); // Register AppSettings as singleton
        return appSettings;
    }
}