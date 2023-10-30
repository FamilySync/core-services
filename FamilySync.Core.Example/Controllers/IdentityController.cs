using FamilySync.Core.Example.Models.DTOS;
using FamilySync.Core.Example.Models.Entities;
using FamilySync.Core.Example.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FamilySync.Core.Example.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class IdentityController : ControllerBase
{
    readonly IIdentityService _identityService;
    readonly UserManager<FamilySyncIdentity> _identityManager;

    public IdentityController
    (
        IIdentityService identityService, 
        UserManager<FamilySyncIdentity> identityManager
    )
    {
        _identityService = identityService;
        _identityManager = identityManager;
    }

    
    /// <summary>
    /// Creates a new identity.
    /// </summary>
    /// <response code="201">Returns the newly created identity</response>
    /// <response code="400">If the identity is malformed</response>
    [HttpPost]
    [ProducesResponseType(201, Type = typeof(FamilySyncIdentityDTO))]
    [ProducesResponseType(400)]
    public async Task<ActionResult<FamilySyncIdentityDTO>> Create([FromBody] FamilySyncIdentityDTO identityDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var identity = await _identityService.Create(identityDto);

        return CreatedAtAction(nameof(Get), new { identityId = identity.Id }, identity);
    }
    
    /// <summary>
    /// Gets a specific identity.
    /// </summary>
    /// <response code="200">Returns the identity</response>
    /// <response code="404">If the identity was not found</response>
    [HttpGet("{identityId}")]
    [ProducesResponseType(200, Type = typeof(FamilySyncIdentityDTO))]
    [ProducesResponseType(404)]
    public async Task<ActionResult<FamilySyncIdentityDTO>> Get([FromRoute] Guid identityId)
    {
        var identity = await _identityManager.FindByIdAsync(identityId.ToString());

        if (identity is null)
        {
            return NotFound();
        }

        var dto = new FamilySyncIdentityDTO
        {
            Id = identity.Id,
            ActorId = identity.ActorId,
            Email = identity.Email,
            Password = identity.PasswordHash
        };
        
        return Ok(dto);
    }
}