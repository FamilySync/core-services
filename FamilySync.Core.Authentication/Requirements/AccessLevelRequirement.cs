using FamilySync.Core.Authentication.Enums;
using FamilySync.Core.Authentication.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace FamilySync.Core.Authentication.Requirements;

public class AccessLevelRequirement : IAuthorizationRequirement
{
    public AccessLevelRequirement(string identity, AccessLevel level)
    {
        this.Identities = identity.GetIdentityHierarchy().ToArray();
        this.Level = (int)level;
    }
    
    public string[] Identities { get; init; }
    public int Level { get; init; }
}