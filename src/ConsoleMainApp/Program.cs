using ConsoleMainApp.TaskRunners;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace ConsoleMainApp;

internal class Program
{
    static void Main(string[] args)
    {
        var host = AppStartup();

        var logger = host.Services.GetRequiredService<ILogger<Program>>();

        logger.LogInformation("ConsoleMainApp is started");

        var taskRunner = host.Services.GetRequiredService<TaskRunner>();

        var dayNumber = 9;

        taskRunner.RunSolver(dayNumber);

        logger.LogInformation("ConsoleMainApp has completed");

        Console.WriteLine("Press a key to exit ...");
        Console.ReadLine();

    }

    static IHost AppStartup()
    {
        var host = Host.CreateDefaultBuilder()
            .ConfigureHostConfiguration(configHost =>
            {
                configHost.SetBasePath(Directory.GetCurrentDirectory());
                configHost.AddJsonFile("appsettings.json", optional: true);
            })
            .ConfigureServices(services =>
            {
                services.AddSingleton<TaskRunner>();

                services.Scan(selector =>
                    selector.FromCallingAssembly()
                    .AddClasses(classSelector => classSelector.AssignableTo<IPuzzleSolver>()));

            })
            .UseSerilog((context, configuration) =>
            {
                configuration.ReadFrom.Configuration(context.Configuration);
            })
            .Build();

        return host;
    }
}
