using System.Security.Claims;
using FamilySync.Core.Authentication.Enums;
using FamilySync.Core.Authentication.Requirements;
using Microsoft.AspNetCore.Authorization;

namespace FamilySync.Core.Authentication.Handler;

public class AccessLevelHandler : AuthorizationHandler<AccessLevelRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AccessLevelRequirement requirement)
    {
        if(Validate(requirement, context.User.Claims))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
    
    public bool Validate(AccessLevelRequirement requirement, IEnumerable<Claim> claims)
    {
        foreach (var identity in requirement.Identities)
        {
            Claim? claim = claims.FirstOrDefault(x => x.Type == identity);
            
            if(claim is null)
            {
                continue;
            }

            return ValidateAccessLevel(claim, requirement.Level);
        }

        return false;
    }
    
    /// <summary>
    /// Determines whether the claim provides the required or higher AccessLevel! <see cref="AccessLevel"/>.
    /// </summary>
    public static bool ValidateAccessLevel(Claim claim, int requiredLevel)
    {
        return Enum.TryParse(claim.Value, out AccessLevel accessLevel) && (int)accessLevel >= requiredLevel;
    }
}