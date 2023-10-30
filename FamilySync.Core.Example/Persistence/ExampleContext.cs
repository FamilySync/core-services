using Microsoft.EntityFrameworkCore;

namespace FamilySync.Core.Example.Persistence;

public class ExampleContext : DbContext
{
    public ExampleContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Models.Entities.Example> Examples { get; set; }
}