using FamilySync.Core.Example.Models.DTOS;
using FamilySync.Core.Example.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace FamilySync.Core.Example.Services;

public interface IClaimService
{
    public Task<bool> Create(FamilySyncIdentity identity, FamilySyncClaimDTO dto);
    public Task<FamilySyncClaimDTO?> Get(FamilySyncIdentity identity, string claimType);

}

public class ClaimService : IClaimService
{
    readonly UserManager<FamilySyncIdentity> _identityManager;

    public ClaimService(UserManager<FamilySyncIdentity> identityManager)
    {
        _identityManager = identityManager;
    }

    public async Task<bool> Create(FamilySyncIdentity identity, FamilySyncClaimDTO dto)
    {
        var claims = await _identityManager.GetClaimsAsync(identity);

        if (claims.Any(o => o.Type == dto.Claim))
        {
            await _identityManager.RemoveClaimAsync(identity, claims.First(o => o.Type == dto.Claim));
        }

        IdentityResult result = await _identityManager.AddClaimAsync(identity, new(dto.Claim, dto.AccessLevel));
        
        return result.Succeeded;
    }
    
    public async Task<FamilySyncClaimDTO?> Get(FamilySyncIdentity identity, string claimType)
    {
        //TODO: Use mapper ..
        return await _identityManager.GetClaimsAsync(identity)
            .ContinueWith(o => o.Result.FirstOrDefault(k => k.Type == claimType))
            .ContinueWith(o =>
            {
                var claim = o.Result;
                if (claim != null)
                {
                    var dto = new FamilySyncClaimDTO
                    {
                        Claim = claim.Type,
                        AccessLevel = claim.Value
                    };
                    return dto;
                }

                // Handle the case where the claim was not found or is null
                return null;
            });
    }
}