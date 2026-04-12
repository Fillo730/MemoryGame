using Microsoft.EntityFrameworkCore;

namespace MemoryGame_API.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

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

        var difficulties = new List<Difficulty>
        {
            new Difficulty { Id = 1, NumberOfPairs = 4 },  
            new Difficulty { Id = 2, NumberOfPairs = 6 },  
            new Difficulty { Id = 3, NumberOfPairs = 8 },  
            new Difficulty { Id = 4, NumberOfPairs = 10 }, 
            new Difficulty { Id = 5, NumberOfPairs = 12 }, 
            new Difficulty { Id = 6, NumberOfPairs = 15 }, 
            new Difficulty { Id = 7, NumberOfPairs = 18 }, 
            new Difficulty { Id = 8, NumberOfPairs = 21 }, 
            new Difficulty { Id = 9, NumberOfPairs = 25 } 
        };
        modelBuilder.Entity<Difficulty>().HasData(difficulties);

        var translations = new List<DifficultyTranslation>
        {
            new() { Id = 1, DifficultyId = 1, LanguageCode = "en", Label = "Easy" },
            new() { Id = 2, DifficultyId = 1, LanguageCode = "it", Label = "Facile" },
            new() { Id = 3, DifficultyId = 2, LanguageCode = "en", Label = "Medium" },
            new() { Id = 4, DifficultyId = 2, LanguageCode = "it", Label = "Medio" },
            new() { Id = 5, DifficultyId = 3, LanguageCode = "en", Label = "Hard" },
            new() { Id = 6, DifficultyId = 3, LanguageCode = "it", Label = "Difficile" },
            new() { Id = 7, DifficultyId = 4, LanguageCode = "en", Label = "Extreme" },
            new() { Id = 8, DifficultyId = 4, LanguageCode = "it", Label = "Estremo" },
            new() { Id = 9, DifficultyId = 5, LanguageCode = "en", Label = "Impossible" },
            new() { Id = 10, DifficultyId = 5, LanguageCode = "it", Label = "Impossibile" },
            new() { Id = 11, DifficultyId = 6, LanguageCode = "en", Label = "Legendary" },
            new() { Id = 12, DifficultyId = 6, LanguageCode = "it", Label = "Leggendario" },
            new() { Id = 13, DifficultyId = 7, LanguageCode = "en", Label = "Mythical" },
            new() { Id = 14, DifficultyId = 7, LanguageCode = "it", Label = "Mitico" },
            new() { Id = 15, DifficultyId = 8, LanguageCode = "en", Label = "Divine" },
            new() { Id = 16, DifficultyId = 8, LanguageCode = "it", Label = "Divino" },
            new() { Id = 17, DifficultyId = 9, LanguageCode = "en", Label = "Godlike" },
            new() { Id = 18, DifficultyId = 9, LanguageCode = "it", Label = "Sovrumano" }
        };
        modelBuilder.Entity<DifficultyTranslation>().HasData(translations);
    }
}