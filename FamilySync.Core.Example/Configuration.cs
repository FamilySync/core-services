using FamilySync.Core.Persistence;
using FamilySync.Core.Example.Persistence;
using FamilySync.Core.Example.Services;
using FamilySync.Core.Helpers;

namespace FamilySync.Core.Example;

public class Configuration : ServiceConfiguration
{
    public override void Configure(IApplicationBuilder app)
    {
    }

    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddMySqlContext<ExampleContext>("examples", Configuration);
        services.AddTransient<IExampleService, ExampleService>();
    }
}