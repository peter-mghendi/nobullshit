using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NoBullshit.Shared.Models;

namespace NoBullshit.Server.Data;

public class NoBullshitContext : DbContext
{
    public DbSet<Game>? Games { get; set; }

    public NoBullshitContext(DbContextOptions<NoBullshitContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Game>()
           .HasDiscriminator<string>(g => g.Platform!)
           .HasValue<AndroidGame>("android")
           .HasValue<IOSGame>("ios");

        JsonSerializerOptions? options = default;
        modelBuilder.Entity<Game>()
                .Property(g => g.Genres)
                .HasConversion(
                    genres => JsonSerializer.Serialize(genres, options),
                    json => JsonSerializer.Deserialize<string[]>(json, options),
                    new ValueComparer<string[]>(
                        (s1, s2) => Equals(s1, s2),
                        s => s.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                        s => s.ToArray()
                    ));
    }
}
