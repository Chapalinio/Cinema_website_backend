using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DLL.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Film> Films { get; set; }
    public DbSet<Comment> Comments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(e =>
        {
            e.HasKey(u => u.Id);

            e.HasMany(u => u.Comments)
            .WithOne(c => c.User)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Film>(e =>
        {
            e.HasKey(f => f.Id);

            e.HasMany(f => f.Comments)
            .WithOne(c => c.Film)
            .HasForeignKey(c => c.FilmId)
            .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Comment>(e => e.HasKey(c => c.Id));
    }
}
