using FamilySync.Core.Example.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FamilySync.Core.Example.Controllers;


[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class ExampleController : ControllerBase
{
    private readonly IExampleService _service;

    public ExampleController(IExampleService service)
    {
        _service = service;
    }

    [Authorize]
    [HttpGet("example")]
    [ProducesResponseType(200)]
    public ActionResult<string> Example([FromQuery] string name)
    {
        return Ok($"Hello {name}!");
    }
    
    [HttpPost("{name}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult> CreateExample([FromRoute] string name)
    {
        var example = await _service.Create(name);

        return CreatedAtAction(nameof(GetExample), new {id = example.Id}, example);
    }
    
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Models.Entities.Example))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]    
    public async Task<ActionResult<string>> GetExample([FromRoute] Guid id)
    {
        var example = await _service.Get(id);

        return example != default ? Ok(example) : NotFound($"No example found with id > {id}");
    }
}