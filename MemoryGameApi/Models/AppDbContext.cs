using Microsoft.EntityFrameworkCore;
using MemoryGame_API.Utils;

namespace MemoryGame_API.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<GameResult> GameResults { get; set; }
    public DbSet<Difficulty> Difficulties { get; set; }
    public DbSet<DifficultyTranslation> DifficultyTranslations { get; set; }
    public DbSet<Achievement> Achievements { get; set; }
    public DbSet<AchievementTranslation> AchievementTranslations { get; set; }
    public DbSet<UserAchievement> UserAchievements { get; set; }

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

        modelBuilder.Entity<AchievementTranslation>()
            .HasOne(t => t.Achievement)
            .WithMany(a => a.Translations)
            .HasForeignKey(t => t.AchievementId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<UserAchievement>()
            .HasOne(ua => ua.User)
            .WithMany()
            .HasForeignKey(ua => ua.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<UserAchievement>()
            .HasOne(ua => ua.Achievement)
            .WithMany(a => a.UserAchievements)
            .HasForeignKey(ua => ua.AchievementId)
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

        var achievements = new List<Achievement>
        {
            new Achievement { Id = 1, Code = "FIRST_WIN", Icon = "flag" },
            new Achievement { Id = 2, Code = "TEN_GAMES", Icon = "local_fire_department" },
            new Achievement { Id = 3, Code = "FIFTY_GAMES", Icon = "military_tech" },
            new Achievement { Id = 4, Code = "HUNDRED_GAMES", Icon = "workspace_premium" },
            new Achievement { Id = 5, Code = "FLAWLESS", Icon = "bolt" },
            new Achievement { Id = 6, Code = "ALL_DIFFICULTIES", Icon = "travel_explore" },
            new Achievement { Id = 7, Code = "HARDEST_DIFFICULTY", Icon = "emoji_events" },
            new Achievement { Id = 8, Code = "NIGHT_OWL", Icon = "nightlight" }
        };
        modelBuilder.Entity<Achievement>().HasData(achievements);

        var achievementTranslations = new List<AchievementTranslation>
        {
            new() { Id = 1, AchievementId = 1, LanguageCode = "en", Name = "First Steps", Description = "Complete your first game." },
            new() { Id = 2, AchievementId = 1, LanguageCode = "it", Name = "Primi Passi", Description = "Completa la tua prima partita." },
            new() { Id = 3, AchievementId = 2, LanguageCode = "en", Name = "Regular", Description = "Play 10 games." },
            new() { Id = 4, AchievementId = 2, LanguageCode = "it", Name = "Habitué", Description = "Gioca 10 partite." },
            new() { Id = 5, AchievementId = 3, LanguageCode = "en", Name = "Veteran", Description = "Play 50 games." },
            new() { Id = 6, AchievementId = 3, LanguageCode = "it", Name = "Veterano", Description = "Gioca 50 partite." },
            new() { Id = 7, AchievementId = 4, LanguageCode = "en", Name = "Legend", Description = "Play 100 games." },
            new() { Id = 8, AchievementId = 4, LanguageCode = "it", Name = "Leggenda", Description = "Gioca 100 partite." },
            new() { Id = 9, AchievementId = 5, LanguageCode = "en", Name = "Iron Memory", Description = "Complete a game without a single wrong guess." },
            new() { Id = 10, AchievementId = 5, LanguageCode = "it", Name = "Memoria di Ferro", Description = "Completa una partita senza sbagliare nemmeno una mossa." },
            new() { Id = 11, AchievementId = 6, LanguageCode = "en", Name = "Explorer", Description = "Win at least one game on every difficulty." },
            new() { Id = 12, AchievementId = 6, LanguageCode = "it", Name = "Esploratore", Description = "Vinci almeno una partita per ogni difficoltà." },
            new() { Id = 13, AchievementId = 7, LanguageCode = "en", Name = "Godlike", Description = "Win a game on the hardest difficulty." },
            new() { Id = 14, AchievementId = 7, LanguageCode = "it", Name = "Sovrumano", Description = "Vinci una partita nella difficoltà più alta." },
            new() { Id = 15, AchievementId = 8, LanguageCode = "en", Name = "Night Owl", Description = "Complete a game between midnight and 5 AM." },
            new() { Id = 16, AchievementId = 8, LanguageCode = "it", Name = "Nottambulo", Description = "Completa una partita tra mezzanotte e le 5 del mattino." }
        };
        modelBuilder.Entity<AchievementTranslation>().HasData(achievementTranslations);

        // Demo seed data so a fresh database (local dev, or a new deploy) doesn't
        // start with an empty leaderboard. Password for both is "Demo1234!".
        var seedUsers = new List<User>
        {
            new User
            {
                Id = 1,
                Username = "demo_player1",
                Email = "demo1@example.com",
                PasswordHash = "$2a$11$5XPr6Kjk/jJmse1wP71jpO06ZLsknX/zDDRhp4Q8S..hOhUSXgwLC",
                CreatedAt = new DateTime(2025, 6, 1, 0, 0, 0, DateTimeKind.Utc),
                Role = UserRoleEnum.User
            },
            new User
            {
                Id = 2,
                Username = "demo_player2",
                Email = "demo2@example.com",
                PasswordHash = "$2a$11$5XPr6Kjk/jJmse1wP71jpO06ZLsknX/zDDRhp4Q8S..hOhUSXgwLC",
                CreatedAt = new DateTime(2025, 6, 1, 0, 0, 0, DateTimeKind.Utc),
                Role = UserRoleEnum.User
            }
        };
        modelBuilder.Entity<User>().HasData(seedUsers);

        var seedGameResults = new List<GameResult>
        {
            new GameResult { Id = 1, Moves = 6, PlayedAt = new DateTime(2025, 6, 1, 10, 0, 0, DateTimeKind.Utc), UserId = 1, DifficultyId = 1 },
            new GameResult { Id = 2, Moves = 8, PlayedAt = new DateTime(2025, 6, 2, 10, 0, 0, DateTimeKind.Utc), UserId = 1, DifficultyId = 1 },
            new GameResult { Id = 3, Moves = 14, PlayedAt = new DateTime(2025, 6, 3, 10, 0, 0, DateTimeKind.Utc), UserId = 1, DifficultyId = 2 },
            new GameResult { Id = 4, Moves = 20, PlayedAt = new DateTime(2025, 6, 4, 10, 0, 0, DateTimeKind.Utc), UserId = 1, DifficultyId = 3 },
            new GameResult { Id = 5, Moves = 5, PlayedAt = new DateTime(2025, 6, 1, 12, 0, 0, DateTimeKind.Utc), UserId = 2, DifficultyId = 1 },
            new GameResult { Id = 6, Moves = 12, PlayedAt = new DateTime(2025, 6, 2, 12, 0, 0, DateTimeKind.Utc), UserId = 2, DifficultyId = 2 },
            new GameResult { Id = 7, Moves = 10, PlayedAt = new DateTime(2025, 6, 5, 12, 0, 0, DateTimeKind.Utc), UserId = 2, DifficultyId = 2 },
            new GameResult { Id = 8, Moves = 35, PlayedAt = new DateTime(2025, 6, 6, 12, 0, 0, DateTimeKind.Utc), UserId = 2, DifficultyId = 4 }
        };
        modelBuilder.Entity<GameResult>().HasData(seedGameResults);
    }
}