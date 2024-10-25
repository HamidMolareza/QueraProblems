using System;
using System.CommandLine;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnRail.Extensions.OnFail;
using OnRail.Extensions.Try;
using Quera.Cache;
using Quera.Collector;
using Quera.Configs;
using Quera.Generator;
using Quera.Helpers;
using Serilog;

namespace Quera;

public static class Program {
    public static Task Main(string[] args) =>
        TryExtensions.Try(() => InnerMain(args))
            .OnFailTee(result => {
                //for GitHub action: https://github.com/HamidMolareza/QueraProblems/issues/10
                Environment.ExitCode = -1;

                Log.Error("The operation failed. See the below text for more information:\n{result}",
                    result.ToStr());
            });

    private static Task<int> InnerMain(string[] args) {
        ConfigSerilog();

        // Setup DI container
        var services = new ServiceCollection();

        var appSettings = services.ConfigAppSettings("appsettings.json");
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
            .MinimumLevel.Debug()
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

        var outputOption = new Option<string>(
            ["-o", "--output"],
            () => appSettings.ReadmeOutputPath,
            "The readme output path");

        var workingDirectoryOption = new Option<string>(
            ["-w", "--working-directory"],
            () => appSettings.WorkingDirectory,
            "The working directory of the application");
        workingDirectoryOption.AddValidator(result => {
            var value = result.GetValueOrDefault<string>();
            if (string.IsNullOrEmpty(value) || value == ".")
                return;
            if (!Directory.Exists(value))
                result.ErrorMessage = "The working directory is not valid.";
        });

        var solutionsOption = new Option<string>(
            ["-s", "--solutions"],
            () => appSettings.SolutionsPath,
            "The solutions directory");

        // Create root command and add options
        var rootCommand = new RootCommand {
            delayToRequestQueraInMilliSecondsOption,
            readmeTemplatePathOption,
            outputOption,
            workingDirectoryOption,
            solutionsOption
        };

        // Set handler for root command
        rootCommand.SetHandler(CommandHandler(serviceProvider), delayToRequestQueraInMilliSecondsOption, readmeTemplatePathOption, workingDirectoryOption, outputOption,
            solutionsOption);

        return rootCommand.InvokeAsync(args);
    }

    private static Func<int, string, string, string, string, Task> CommandHandler(IServiceProvider serviceProvider) {
        return async (delayToRequestQueraInMilliSeconds, readmeTemplatePath,
            workingDirectory, outputPath, solutionsPath) => {
            var settings = UpdateAppSettings(serviceProvider, delayToRequestQueraInMilliSeconds, workingDirectory, readmeTemplatePath, outputPath, solutionsPath);
            Log.Debug("App Settings:\n{settings}", settings.ToString());

            ChangeWorkingDirectory(settings);


            // Call other classes/methods with DI
            var runner = serviceProvider.GetService<AppRunner>();
            if (runner is null) throw new Exception("Can not get app runner from DI.");

            var result = await runner.RunAsync();
            result.OnFailThrowException();
        };
    }

    private static void ChangeWorkingDirectory(AppSettings settings) {
        if (!string.IsNullOrEmpty(settings.WorkingDirectory) && settings.WorkingDirectory != ".")
            Directory.SetCurrentDirectory(settings.WorkingDirectory);
    }

    private static AppSettings UpdateAppSettings(IServiceProvider serviceProvider, int delayToRequestQueraInMilliSeconds,
        string workingDirectory, string readmeTemplatePath, string outputPath, string solutionsPath) {
        // Get AppSettings instance from DI
        var settings = serviceProvider.GetService<AppSettings>();
        if (settings is null) throw new Exception("Can not get app settings from DI.");

        settings.DelayToRequestQueraInMilliSeconds = delayToRequestQueraInMilliSeconds;
        settings.WorkingDirectory = workingDirectory;
        settings.ReadmeTemplatePath = readmeTemplatePath;
        settings.ReadmeOutputPath = outputPath;
        settings.SolutionsPath = solutionsPath;
        
        return settings;
    }

    private static AppSettings ConfigAppSettings(this IServiceCollection services, string appSettingsPath) {
        // Build Configuration from the provided appsettings.json path
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(appSettingsPath, optional: false, reloadOnChange: true)
            .Build();

        // Bind AppSettings from Configuration
        var appSettings = configuration.Get<AppSettings>() ?? new AppSettings();
        services.AddSingleton(appSettings); // Register AppSettings as singleton
        return appSettings;
    }
}