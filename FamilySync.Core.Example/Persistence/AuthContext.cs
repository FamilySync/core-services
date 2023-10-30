using Ajour.Models.Authentication.Entities;
using FamilySync.Core.Example.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FamilySync.Core.Example.Persistence
{
    public class AuthContext : IdentityDbContext<FamilySyncIdentity, IdentityRole<Guid>, Guid>
    {
        public AuthContext(DbContextOptions<AuthContext> options) : base(options)
        {
        }

        public DbSet<FamilySyncIdentity> FamilySyncIdentities { get; set; }
        public DbSet<FamilySyncUser> FamilySyncUsers { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<FamilySyncUser>()
                .HasMany(o => o.Claims)
                .WithOne(o => o.User)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<FamilySyncUserClaim>()
                .HasOne(o => o.User)
                .WithMany(o => o.Claims);
        }
    }
}