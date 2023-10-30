using FamilySync.Core.Authentication.Enums;
using FamilySync.Core.Authentication.Models;
using FamilySync.Core.Authentication.Requirements;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace FamilySync.Core.Authentication.Extensions;

public static class AuthorizationOptionsExtensions
{
    public static void AddFamilySyncPolicies(this AuthorizationOptions options)
    {
        var accessLevels = Enum.GetValues<AccessLevel>();

        foreach (var data in IdentityData.Data)
        {
            if(string.IsNullOrEmpty(data.Policy))
            {
                continue;
            }

            foreach (var level in accessLevels)
            {
                options.AddPolicy($"{data.Policy}:{Enum.GetName(level)}", policyBuilder =>
                {
                    policyBuilder.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
                    policyBuilder.RequireAuthenticatedUser();
                    policyBuilder.AddRequirements(new AccessLevelRequirement(data.Identity, level));
                });
            }
        }

        options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
            .RequireAuthenticatedUser()
            .Build();
    }
}