using System;
using System.CommandLine;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnRail.Extensions.OnFail;
using OnRail.Extensions.Try;
using Quera.Cache;
using Quera.Collector;
using Quera.Configs;
using Quera.Crawler;
using Quera.Generator;
using Quera.Helpers;
using Serilog;
using Serilog.Events;

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

    private static async Task<int> InnerMain(string[] args) {
        // Setup DI container
        var services = new ServiceCollection();

        var appSettings = services.ConfigAppSettings("appsettings.json");

        ConfigSerilog(Utility.ParseLogLevel(appSettings.LogLevel));

        services.AddScoped<CollectorService>();
        services.AddScoped<GeneratorService>();
        services.AddScoped<CacheRepository>();
        services.AddScoped<CrawlerService>();
        services.AddScoped<AppRunner>();

        //Database
        ChangeWorkingDirectory(appSettings.WorkingDirectory);
        Log.Debug("cache file path: {path}", appSettings.CacheFilePath);
        services.AddDbContext<CacheDbContext>(options =>
            options.UseSqlite($"Data Source={appSettings.CacheFilePath}"));

        Log.Debug("Services are registered.");

        var serviceProvider = services.BuildServiceProvider();

        // Create database
        var db = serviceProvider.GetRequiredService<CacheDbContext>();
        await db.Database.EnsureCreatedAsync();

        return await InvokeCommandLine(args, appSettings, serviceProvider);
    }

    private static void ConfigSerilog(LogEventLevel minimumLevel = LogEventLevel.Debug) {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Is(minimumLevel)
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

        var solutionsOption = new Option<string>(
            ["-s", "--solutions"],
            () => appSettings.SolutionsPath,
            "The solutions directory");

        // Create root command and add options
        var rootCommand = new RootCommand {
            delayToRequestQueraInMilliSecondsOption,
            readmeTemplatePathOption,
            outputOption,
            solutionsOption
        };

        // Set handler for root command
        rootCommand.SetHandler(CommandHandler(serviceProvider),
            delayToRequestQueraInMilliSecondsOption, readmeTemplatePathOption,
            outputOption, solutionsOption
        );

        return rootCommand.InvokeAsync(args);
    }

    private static Func<int, string, string, string, Task> CommandHandler(IServiceProvider serviceProvider) {
        return async (delayToRequestQueraInMilliSeconds, readmeTemplatePath, outputPath, solutionsPath) => {
            var settings = await UpdateAppSettings(serviceProvider, delayToRequestQueraInMilliSeconds,
                readmeTemplatePath, outputPath, solutionsPath);
            Log.Debug("App Settings:\n{settings}", settings.ToString());

            // Call other classes/methods with DI
            var runner = serviceProvider.GetService<AppRunner>();
            if (runner is null) throw new Exception("Can not get app runner from DI.");

            var result = await runner.RunAsync();
            if (!result.IsSuccess)
                throw new Exception(result.ToStr());
        };
    }

    private static void ChangeWorkingDirectory(string workingDirectory) {
        if (!string.IsNullOrEmpty(workingDirectory) && workingDirectory != ".")
            Directory.SetCurrentDirectory(workingDirectory);
    }

    private static async Task<AppSettings> UpdateAppSettings(IServiceProvider serviceProvider,
        int delayToRequestQueraInMilliSeconds, string readmeTemplatePath,
        string outputPath, string solutionsPath) {
        // Get AppSettings instance from DI
        var settings = serviceProvider.GetService<AppSettings>();
        if (settings is null) throw new Exception("Can not get app settings from DI.");

        settings.DelayToRequestQueraInMilliSeconds = delayToRequestQueraInMilliSeconds;
        settings.ReadmeTemplatePath = readmeTemplatePath;
        settings.ReadmeOutputPath = outputPath;
        settings.SolutionsPath = solutionsPath;

        // Use the Gravatar image as default user profile
        foreach (var user in settings.Users.Where(user => string.IsNullOrEmpty(user.AvatarUrl))) {
            user.AvatarUrl = await GravatarHelper.GetGravatarUrlAsync(user.Email!);
        }

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