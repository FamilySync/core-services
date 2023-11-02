using FamilySync.Core.Authentication.Enums;

namespace FamilySync.Core.Authentication.Models;

public class ClaimAccess : ClaimDefinition
{
    public AccessLevel? AccessLevel { get; init; }
}