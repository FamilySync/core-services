using FamilySync.Core.Authentication.Identity;

namespace FamilySync.Core.Authentication.Models;

public class IdentityData
{
    static List<IdentityData>? _identities;

    public static List<IdentityData> Data
    {
        get
        {
            return _identities ??= IdentityDefinitions.Build();
        }
    }

    public string Identity { get; init; } = default!;
    public string Description { get; init; } = default!;
    public string Name { get; init; } = default!;
    public string Policy { get; init; } = default!;
}