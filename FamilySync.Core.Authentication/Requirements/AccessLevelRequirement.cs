using FamilySync.Core.Authentication.Enums;
using FamilySync.Core.Authentication.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace FamilySync.Core.Authentication.Requirements;

public class AccessLevelRequirement : IAuthorizationRequirement
{
    public AccessLevelRequirement(string identity, AccessLevel level)
    {
        Identities = identity.GetIdentityHierarchy().ToArray();
        Level = (int)level;
    }
    
    public string[] Identities { get; }
    public int Level { get; }
}