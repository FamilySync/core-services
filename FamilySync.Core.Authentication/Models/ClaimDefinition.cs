using FamilySync.Core.Authentication.Identity;

namespace FamilySync.Core.Authentication.Models;

public class ClaimDefinition
{
    static List<ClaimDefinition>? _definitions;

    public static List<ClaimDefinition> Definitions
    {
        get
        {
            return _definitions ??= ClaimDefinitions.Build();
        }
    }

    public string Claim { get; init; } = default!;
    public string Description { get; init; } = default!;
    public string Name { get; init; } = default!;
    public string Policy { get; init; } = default!;
}