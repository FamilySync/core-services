using System.ComponentModel.DataAnnotations;

namespace FamilySync.Core.Example.Models.DTOS;

public class FamilySyncClaimDTO
{
    [Required]
    public string AccessLevel { get; set; }
    
    [Required]
    public string Claim { get; set; }
}