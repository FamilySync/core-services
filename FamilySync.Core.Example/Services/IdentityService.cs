using System.Security.Claims;
using FamilySync.Core.Authentication.Enums;
using FamilySync.Core.Example.Models.DTOS;
using FamilySync.Core.Example.Models.Entities;
using FamilySync.Core.Helpers.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace FamilySync.Core.Example.Services;

public interface IIdentityService
{
    public Task<FamilySyncIdentityDTO> Create(FamilySyncIdentityDTO dto);
}

public class IdentityService : IIdentityService
{
    readonly UserManager<FamilySyncIdentity> _identityManager;

    public IdentityService(UserManager<FamilySyncIdentity> identityManager)
    {
        _identityManager = identityManager;
    }

    public async Task<FamilySyncIdentityDTO> Create(FamilySyncIdentityDTO dto)
    {
        FamilySyncIdentity identity = new()
        {
            UserName = dto.Email,
            Email = dto.Email,
            ActorId = dto.ActorId,
            PasswordHash = dto.Password
        };

        IdentityResult result;

        if (dto.Password != default && !string.IsNullOrEmpty(dto.Password))
        {
            result = await _identityManager.CreateAsync(identity, dto.Password);
        }
        else
        {
            result = await _identityManager.CreateAsync(identity);
        } 
        
        if(result.Succeeded)
        {
            result = await AddDefaultClaims(identity);
            
            if(result.Succeeded)
            {
                return new()
                {
                    ActorId = identity.ActorId,
                    Email = identity.Email,
                    Id = identity.Id,
                    Password = identity.PasswordHash
                };
            }

            await _identityManager.DeleteAsync(identity);
        }

        throw new BadRequestException("Failed to create identity");
    }
    
    async Task<IdentityResult> AddDefaultClaims(FamilySyncIdentity user)
    {
        var defaultClaims = new List<Claim>()
        {
            new("iid", user.Id.ToString()),
            new("uid", user.ActorId.ToString()),
            new("email", user.Email),
            new("fam", Enum.GetName(AccessLevel.USER_BASIC))
        };

        return await _identityManager.AddClaimsAsync(user, defaultClaims);
    }
}