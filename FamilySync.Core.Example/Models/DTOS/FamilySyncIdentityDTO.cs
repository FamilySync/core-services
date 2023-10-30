namespace FamilySync.Core.Example.Models.DTOS;

public class FamilySyncIdentityDTO
{
    public Guid ActorId { get; set; }
    public string? Email { get; set; }
    public Guid Id { get; set; }
    public string? Password { get; set; }
}