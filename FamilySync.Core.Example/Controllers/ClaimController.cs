using FamilySync.Core.Example.Models.DTOS;
using FamilySync.Core.Example.Models.Entities;
using FamilySync.Core.Example.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FamilySync.Core.Example.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class ClaimController : ControllerBase
{
   readonly IClaimService _service;
   readonly UserManager<FamilySyncIdentity> _identityManager;
   
   public ClaimController(IClaimService service, UserManager<FamilySyncIdentity> identityManager)
   {
      _service = service;
      _identityManager = identityManager;
   }
   
   /// <summary>
   /// Adds or updates a claim for a specific identity.
   /// </summary>
   /// <response code="201">If the claim was added</response>
   /// <response code="400">If the claim was malformed</response>
   /// <response code="404">If the identity was not found</response>
   [HttpPut("{identityId}/claims")]
   [ProducesResponseType(201)]
   [ProducesResponseType(400)]
   [ProducesResponseType(404)]
   public async Task<ActionResult> ClaimAddByIdentityId([FromRoute] Guid identityId, [BindRequired][FromBody] FamilySyncClaimDTO dto)
   {
      if (!ModelState.IsValid)
      {
         return BadRequest(ModelState);
      }

      var identity = await _identityManager.FindByIdAsync(identityId.ToString());

      if (identity is null)
      {
         return NotFound();
      }

      var result = await _service.Create(identity, dto);

      if (!result)
      {
         return BadRequest();
      }

      return CreatedAtAction(nameof(ClaimGetByIdentityIdClaimType), new { identityId, claimType = dto.Claim }, default);
   }
   
   /// <summary>
   /// Gets a specific claim for a specific identity.
   /// </summary>
   /// <response code="200">Returns the claim</response>
   /// <response code="404">If the identity was not found</response>
   [HttpGet("{identityId}/claims/{claimType}")]
   [ProducesResponseType(200, Type = typeof(FamilySyncClaimDTO))]
   [ProducesResponseType(404)]
   public async Task<ActionResult<FamilySyncClaimDTO>> ClaimGetByIdentityIdClaimType([FromRoute] Guid identityId, [FromRoute] string claimType)
   {
       
      var identity = await _identityManager.FindByIdAsync(identityId.ToString());

      if (identity is null)
      {
         return NotFound();
      }

      var claim = await _service.Get(identity, claimType);

      if (claim is null)
      {
         return NotFound();
      }

      return Ok(claim);
   }
}