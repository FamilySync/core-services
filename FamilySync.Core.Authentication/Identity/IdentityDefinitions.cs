using FamilySync.Core.Authentication.Models;

namespace FamilySync.Core.Authentication.Identity;

public static class IdentityDefinitions
{
    public static List<IdentityData> Build()
    {
        List<IdentityData> data = new();

        #region User

        data.Add(new()
        {
            Identity = "Userprofile",
            Description = "Controls access to the Userprofile service",
            Name = "fam:user",
            Policy = "userprofile"
        });

        #endregion


        return data;
    }
}