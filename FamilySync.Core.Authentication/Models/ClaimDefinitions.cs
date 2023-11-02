using FamilySync.Core.Authentication.Models;

namespace FamilySync.Core.Authentication.Identity;

public static class ClaimDefinitions
{
    public static List<ClaimDefinition> Build()
    {
        List<ClaimDefinition> claims = new();

        #region core

        claims.Add(new()
        {
            Name = "FamilySync",
            Description = "FamilySync root. This gives access to everything",
            Claim = "fam",
            Policy = "famsync"
        });

        #endregion
        
        #region User

        claims.Add(new()
        {
            Name = "UserProfile",
            Description = "Controls access to the Userprofile service",
            Claim = "fam:user",
            Policy = "user"
        });

        #endregion


        return claims;
    }
}