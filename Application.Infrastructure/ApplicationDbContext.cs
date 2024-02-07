using Application.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Reflection.Metadata;
using System.Text.Json;

namespace Application.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Identity");
            modelBuilder.Entity<User>(entityBuilder =>
            {
                entityBuilder.Property(c => c.UserName)                
                .HasMaxLength(50);

                entityBuilder
                .HasIndex(c => c.UserName)
                .IsUnique();

                entityBuilder.Property(c => c.Name).HasMaxLength(100);

                entityBuilder.Property(c => c.Email).HasMaxLength(100);

                entityBuilder.HasIndex(c => c.Email).IsUnique();

                entityBuilder.Property(c => c.Country).HasMaxLength(20);

                entityBuilder.Property(e => e.Roles)
                .HasMaxLength(500)
    .HasConversion(
        v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
        v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions)null),
        new ValueComparer<ICollection<string>>(
            (c1, c2) => c1.SequenceEqual(c2),
            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
            c => c.ToList()));

            });
        }
    }
}
