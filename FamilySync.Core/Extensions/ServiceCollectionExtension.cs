using System.Text;
using FamilySync.Core.Authentication.Extensions;
using FamilySync.Core.Helpers.Settings;
using FamilySync.Core.Settings;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;


namespace FamilySync.Core.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection InitializeService(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<IncludeSettings>(configuration.GetRequiredSection("Settings:Authentication"));
        services.Configure<ServiceSettings>(configuration.GetRequiredSection("Settings:Service"));
        services.Configure<IncludeSettings>(configuration.GetSection("Settings:Include"));

        var settings = configuration.GetRequiredSection("Settings")
            .Get<ConfigurationSettings>()!;

        if(settings.Include.Versioning)
        {
            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = false;
                options.ReportApiVersions = true;
                options.DefaultApiVersion = new(1, 0);
            });

            services.AddVersionedApiExplorer(options =>
            {
                options.SubstituteApiVersionInUrl = true;
                options.GroupNameFormat = "'v'VVV";
            });
        }
        
        if(settings.Include.Authentication)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Authentication.Secret));
            services.AddCustomAuthorization(key);
        }

        if (settings.Include.Mvc)
        {
            var mvcBuilder = services.AddControllers(options => { options.AllowEmptyInputInBodyModelBinding = true; });

            // Obtain all assemblies and add them to mvc so it can discover what it needs .. like controllers
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                mvcBuilder.PartManager.ApplicationParts.Add(new AssemblyPart(assembly));
            }
        }

        if (settings.Include.Swagger)
        {
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerConfigurationSettings>();

            services.AddSwaggerGen();
            
            services.AddEndpointsApiExplorer();
        }
        
        
        
        return services;
    }
}