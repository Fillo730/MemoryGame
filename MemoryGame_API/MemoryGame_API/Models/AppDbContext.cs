using Microsoft.EntityFrameworkCore;

namespace MemoryGame_API.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<User> Users { get; set; }

    public DbSet<GameResult> GameResults { get; set; }

    public DbSet<Difficulty> Difficulties { get; set; }

    public DbSet<DifficultyTranslation> DifficultyTranslations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<DifficultyTranslation>()
            .HasOne(t => t.Difficulty)
            .WithMany(d => d.Translations)
            .HasForeignKey(t => t.DifficultyId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<GameResult>()
            .HasOne(g => g.User)
            .WithMany(u => u.GameResults)
            .HasForeignKey(g => g.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<GameResult>()
            .HasOne(g => g.Difficulty)
            .WithMany(d => d.GameResults)
            .HasForeignKey(g => g.DifficultyId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}