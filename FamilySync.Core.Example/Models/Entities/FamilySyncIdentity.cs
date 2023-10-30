using Microsoft.AspNetCore.Identity;

namespace FamilySync.Core.Example.Models.Entities;

public class FamilySyncIdentity : IdentityUser<Guid>
{
    public Guid ActorId { get; set; } = default!;
}