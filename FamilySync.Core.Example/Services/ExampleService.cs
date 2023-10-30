using FamilySync.Core.Example.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FamilySync.Core.Example.Services;

public interface IExampleService
{
    public Task<Models.Entities.Example> Create(string name);
    public Task<Models.Entities.Example> Get(Guid id);
}
public class ExampleService : IExampleService
{
    private readonly ExampleContext _context;

    public ExampleService(ExampleContext context)
    {
        _context = context;
    }

    public async Task<Models.Entities.Example> Create(string name)
    {
        var example = new Models.Entities.Example
        {
            Name = name
        };

        _context.Add(example);
        await _context.SaveChangesAsync();
        
        return example;
    }

    public async Task<Models.Entities.Example> Get(Guid id)
    {
        var example = await _context.Examples.SingleOrDefaultAsync(x => x.Id == id);

        return example!;
    }
}