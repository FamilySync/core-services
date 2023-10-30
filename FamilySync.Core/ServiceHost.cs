using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FamilySync.Core;

public static class ServiceHost<TStartup> where TStartup : Startup, new()
{
    public static int Run(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();

        try
        {
            var appBuilder = WebApplication.CreateBuilder(args);


            var startup = new TStartup()
            {
                Configuration = appBuilder.Configuration
            };

            startup.ConfigureServices(appBuilder.Services);

            var app = appBuilder.Build();

            startup.Configure(app);

            switch (args.Any() ? args[0] : string.Empty)
            {
                default:
                    app.Run();
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex} An fatal error occurred while executing host");
            throw;
        }

        return 0;
    }
}

public static class ServiceHost
{
    public static int Run(string[] args)
    {
        return ServiceHost<Startup>.Run(args);
    }
}