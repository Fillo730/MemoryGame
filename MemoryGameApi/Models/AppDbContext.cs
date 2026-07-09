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
    public DbSet<Friendship> Friendships { get; set; }

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

        modelBuilder.Entity<Friendship>()
            .HasOne(f => f.Requester)
            .WithMany()
            .HasForeignKey(f => f.RequesterId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Friendship>()
            .HasOne(f => f.Addressee)
            .WithMany()
            .HasForeignKey(f => f.AddresseeId)
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
            new() { Id = 18, DifficultyId = 9, LanguageCode = "it", Label = "Sovrumano" },
            new() { Id = 19, DifficultyId = 1, LanguageCode = "fr", Label = "Facile" },
            new() { Id = 20, DifficultyId = 1, LanguageCode = "de", Label = "Einfach" },
            new() { Id = 21, DifficultyId = 2, LanguageCode = "fr", Label = "Moyen" },
            new() { Id = 22, DifficultyId = 2, LanguageCode = "de", Label = "Mittel" },
            new() { Id = 23, DifficultyId = 3, LanguageCode = "fr", Label = "Difficile" },
            new() { Id = 24, DifficultyId = 3, LanguageCode = "de", Label = "Schwer" },
            new() { Id = 25, DifficultyId = 4, LanguageCode = "fr", Label = "Extrême" },
            new() { Id = 26, DifficultyId = 4, LanguageCode = "de", Label = "Extrem" },
            new() { Id = 27, DifficultyId = 5, LanguageCode = "fr", Label = "Impossible" },
            new() { Id = 28, DifficultyId = 5, LanguageCode = "de", Label = "Unmöglich" },
            new() { Id = 29, DifficultyId = 6, LanguageCode = "fr", Label = "Légendaire" },
            new() { Id = 30, DifficultyId = 6, LanguageCode = "de", Label = "Legendär" },
            new() { Id = 31, DifficultyId = 7, LanguageCode = "fr", Label = "Mythique" },
            new() { Id = 32, DifficultyId = 7, LanguageCode = "de", Label = "Mythisch" },
            new() { Id = 33, DifficultyId = 8, LanguageCode = "fr", Label = "Divin" },
            new() { Id = 34, DifficultyId = 8, LanguageCode = "de", Label = "Göttlich" },
            new() { Id = 35, DifficultyId = 9, LanguageCode = "fr", Label = "Surhumain" },
            new() { Id = 36, DifficultyId = 9, LanguageCode = "de", Label = "Übermenschlich" }
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
            new Achievement { Id = 8, Code = "NIGHT_OWL", Icon = "nightlight" },
            new Achievement { Id = 9, Code = "FIVE_HUNDRED_GAMES", Icon = "diamond" },
            new Achievement { Id = 10, Code = "EARLY_BIRD", Icon = "wb_twilight" },
            new Achievement { Id = 11, Code = "WEEKEND_WARRIOR", Icon = "weekend" },
            new Achievement { Id = 12, Code = "PERFECT_STREAK", Icon = "auto_awesome" },
            new Achievement { Id = 13, Code = "MARATHON_DAY", Icon = "directions_run" },
            new Achievement { Id = 14, Code = "FLAWLESS_LEGEND", Icon = "verified" }
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
            new() { Id = 16, AchievementId = 8, LanguageCode = "it", Name = "Nottambulo", Description = "Completa una partita tra mezzanotte e le 5 del mattino." },
            new() { Id = 17, AchievementId = 1, LanguageCode = "fr", Name = "Premiers Pas", Description = "Termine ta première partie." },
            new() { Id = 18, AchievementId = 1, LanguageCode = "de", Name = "Erste Schritte", Description = "Schließe dein erstes Spiel ab." },
            new() { Id = 19, AchievementId = 2, LanguageCode = "fr", Name = "Habitué", Description = "Joue 10 parties." },
            new() { Id = 20, AchievementId = 2, LanguageCode = "de", Name = "Stammspieler", Description = "Spiele 10 Partien." },
            new() { Id = 21, AchievementId = 3, LanguageCode = "fr", Name = "Vétéran", Description = "Joue 50 parties." },
            new() { Id = 22, AchievementId = 3, LanguageCode = "de", Name = "Veteran", Description = "Spiele 50 Partien." },
            new() { Id = 23, AchievementId = 4, LanguageCode = "fr", Name = "Légende", Description = "Joue 100 parties." },
            new() { Id = 24, AchievementId = 4, LanguageCode = "de", Name = "Legende", Description = "Spiele 100 Partien." },
            new() { Id = 25, AchievementId = 5, LanguageCode = "fr", Name = "Mémoire de Fer", Description = "Termine une partie sans la moindre erreur." },
            new() { Id = 26, AchievementId = 5, LanguageCode = "de", Name = "Eisernes Gedächtnis", Description = "Schließe eine Partie ohne einen einzigen Fehlversuch ab." },
            new() { Id = 27, AchievementId = 6, LanguageCode = "fr", Name = "Explorateur", Description = "Gagne au moins une partie à chaque niveau de difficulté." },
            new() { Id = 28, AchievementId = 6, LanguageCode = "de", Name = "Entdecker", Description = "Gewinne mindestens eine Partie auf jedem Schwierigkeitsgrad." },
            new() { Id = 29, AchievementId = 7, LanguageCode = "fr", Name = "Surhumain", Description = "Gagne une partie au niveau de difficulté le plus élevé." },
            new() { Id = 30, AchievementId = 7, LanguageCode = "de", Name = "Übermenschlich", Description = "Gewinne eine Partie auf dem höchsten Schwierigkeitsgrad." },
            new() { Id = 31, AchievementId = 8, LanguageCode = "fr", Name = "Oiseau de Nuit", Description = "Termine une partie entre minuit et 5 heures du matin." },
            new() { Id = 32, AchievementId = 8, LanguageCode = "de", Name = "Nachteule", Description = "Schließe eine Partie zwischen Mitternacht und 5 Uhr morgens ab." },
            new() { Id = 33, AchievementId = 9, LanguageCode = "en", Name = "Unstoppable", Description = "Play 500 games." },
            new() { Id = 34, AchievementId = 9, LanguageCode = "it", Name = "Inarrestabile", Description = "Gioca 500 partite." },
            new() { Id = 35, AchievementId = 9, LanguageCode = "fr", Name = "Inarrêtable", Description = "Joue 500 parties." },
            new() { Id = 36, AchievementId = 9, LanguageCode = "de", Name = "Unaufhaltsam", Description = "Spiele 500 Partien." },
            new() { Id = 37, AchievementId = 10, LanguageCode = "en", Name = "Early Bird", Description = "Complete a game between 5 AM and 8 AM." },
            new() { Id = 38, AchievementId = 10, LanguageCode = "it", Name = "Mattiniero", Description = "Completa una partita tra le 5 e le 8 del mattino." },
            new() { Id = 39, AchievementId = 10, LanguageCode = "fr", Name = "Lève-tôt", Description = "Termine une partie entre 5 h et 8 h du matin." },
            new() { Id = 40, AchievementId = 10, LanguageCode = "de", Name = "Frühaufsteher", Description = "Schließe eine Partie zwischen 5 und 8 Uhr morgens ab." },
            new() { Id = 41, AchievementId = 11, LanguageCode = "en", Name = "Weekend Warrior", Description = "Play a game on Saturday or Sunday." },
            new() { Id = 42, AchievementId = 11, LanguageCode = "it", Name = "Guerriero del Weekend", Description = "Gioca una partita di sabato o domenica." },
            new() { Id = 43, AchievementId = 11, LanguageCode = "fr", Name = "Guerrier du Week-end", Description = "Joue une partie le samedi ou le dimanche." },
            new() { Id = 44, AchievementId = 11, LanguageCode = "de", Name = "Wochenendkrieger", Description = "Spiele eine Partie am Samstag oder Sonntag." },
            new() { Id = 45, AchievementId = 12, LanguageCode = "en", Name = "Perfectionist", Description = "Complete 3 flawless games." },
            new() { Id = 46, AchievementId = 12, LanguageCode = "it", Name = "Perfezionista", Description = "Completa 3 partite senza errori." },
            new() { Id = 47, AchievementId = 12, LanguageCode = "fr", Name = "Perfectionniste", Description = "Termine 3 parties sans la moindre erreur." },
            new() { Id = 48, AchievementId = 12, LanguageCode = "de", Name = "Perfektionist", Description = "Schließe 3 fehlerfreie Partien ab." },
            new() { Id = 49, AchievementId = 13, LanguageCode = "en", Name = "Marathon Runner", Description = "Play 5 games in a single day." },
            new() { Id = 50, AchievementId = 13, LanguageCode = "it", Name = "Maratoneta", Description = "Gioca 5 partite in un solo giorno." },
            new() { Id = 51, AchievementId = 13, LanguageCode = "fr", Name = "Marathonien", Description = "Joue 5 parties en une seule journée." },
            new() { Id = 52, AchievementId = 13, LanguageCode = "de", Name = "Marathonläufer", Description = "Spiele 5 Partien an einem einzigen Tag." },
            new() { Id = 53, AchievementId = 14, LanguageCode = "en", Name = "Flawless Legend", Description = "Complete the hardest difficulty flawlessly." },
            new() { Id = 54, AchievementId = 14, LanguageCode = "it", Name = "Leggenda Perfetta", Description = "Completa la difficoltà più alta senza errori." },
            new() { Id = 55, AchievementId = 14, LanguageCode = "fr", Name = "Légende Parfaite", Description = "Termine le niveau de difficulté le plus élevé sans la moindre erreur." },
            new() { Id = 56, AchievementId = 14, LanguageCode = "de", Name = "Makellose Legende", Description = "Schließe den höchsten Schwierigkeitsgrad fehlerfrei ab." }
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
            new GameResult { Id = 1, Moves = 6, DurationSeconds = 28, PlayedAt = new DateTime(2025, 6, 1, 10, 0, 0, DateTimeKind.Utc), UserId = 1, DifficultyId = 1 },
            new GameResult { Id = 2, Moves = 8, DurationSeconds = 37, PlayedAt = new DateTime(2025, 6, 2, 10, 0, 0, DateTimeKind.Utc), UserId = 1, DifficultyId = 1 },
            new GameResult { Id = 3, Moves = 14, DurationSeconds = 64, PlayedAt = new DateTime(2025, 6, 3, 10, 0, 0, DateTimeKind.Utc), UserId = 1, DifficultyId = 2 },
            new GameResult { Id = 4, Moves = 20, DurationSeconds = 95, PlayedAt = new DateTime(2025, 6, 4, 10, 0, 0, DateTimeKind.Utc), UserId = 1, DifficultyId = 3 },
            new GameResult { Id = 5, Moves = 5, DurationSeconds = 22, PlayedAt = new DateTime(2025, 6, 1, 12, 0, 0, DateTimeKind.Utc), UserId = 2, DifficultyId = 1 },
            new GameResult { Id = 6, Moves = 12, DurationSeconds = 54, PlayedAt = new DateTime(2025, 6, 2, 12, 0, 0, DateTimeKind.Utc), UserId = 2, DifficultyId = 2 },
            new GameResult { Id = 7, Moves = 10, DurationSeconds = 45, PlayedAt = new DateTime(2025, 6, 5, 12, 0, 0, DateTimeKind.Utc), UserId = 2, DifficultyId = 2 },
            new GameResult { Id = 8, Moves = 35, DurationSeconds = 168, PlayedAt = new DateTime(2025, 6, 6, 12, 0, 0, DateTimeKind.Utc), UserId = 2, DifficultyId = 4 }
        };
        modelBuilder.Entity<GameResult>().HasData(seedGameResults);

        var seedFriendships = new List<Friendship>
        {
            new()
            {
                Id = 1,
                RequesterId = 1,
                AddresseeId = 2,
                Status = FriendshipStatusEnum.Accepted,
                CreatedAt = new DateTime(2025, 6, 1, 9, 0, 0, DateTimeKind.Utc),
                RespondedAt = new DateTime(2025, 6, 1, 9, 30, 0, DateTimeKind.Utc)
            }
        };
        modelBuilder.Entity<Friendship>().HasData(seedFriendships);
    }
}