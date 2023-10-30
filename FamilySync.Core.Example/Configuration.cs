using FamilySync.Core.Example.Models.Entities;
using FamilySync.Core.Helpers;
using FamilySync.Core.Persistence;
using FamilySync.Core.Example.Persistence;
using FamilySync.Core.Example.Services;
using Microsoft.AspNetCore.Identity;

namespace FamilySync.Core.Example;

public class Configuration : ServiceConfiguration
{
    public override void Configure(IApplicationBuilder app)
    {
    }

    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddMySqlContext<AuthContext>("auth", Configuration);
        services.AddMySqlContext<ExampleContext>("examples", Configuration);
        services.AddTransient<IExampleService, ExampleService>();
        services.AddTransient<IIdentityService, IdentityService>();
        services.AddTransient<IClaimService, ClaimService>();
        
        services
            .AddIdentity<FamilySyncIdentity, IdentityRole<Guid>>(options =>
            {
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzæøåüöABCDEFGHIJKLMNOPQRSTUVWXYZÆØÅÜÖ0123456789-._@+";
            })
            .AddEntityFrameworkStores<AuthContext>()
            .AddDefaultTokenProviders();
    }
}