using MemoryGame_API.IRepositories;
using MemoryGame_API.Models;
using Microsoft.EntityFrameworkCore;

namespace MemoryGame_API.Repositories;

public class AchievementsRepository (AppDbContext dbContext) : BaseRepository(dbContext), IAchievementsRepository
{
    public async Task<IEnumerable<Achievement>> GetAllAsync()
    {
        return await _dbContext.Achievements
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<Achievement>> GetAllWithTranslationsAsync(string lang)
    {
        return await _dbContext.Achievements
            .AsNoTracking()
            .Include(a => a.Translations.Where(t => t.LanguageCode == lang))
            .ToListAsync();
    }

    public async Task<IEnumerable<Difficulty>> GetDifficultyBenchmarksAsync()
    {
        return await _dbContext.Difficulties
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<UserAchievement>> GetUnlockedForUserAsync(int userId)
    {
        return await _dbContext.UserAchievements
            .AsNoTracking()
            .Where(ua => ua.UserId == userId)
            .ToListAsync();
    }

    public async Task AddUnlockedAsync(int userId, IEnumerable<int> achievementIds)
    {
        var now = DateTime.UtcNow;

        var newUnlocks = achievementIds.Select(achievementId => new UserAchievement
        {
            UserId = userId,
            AchievementId = achievementId,
            UnlockedAt = now
        });

        await _dbContext.UserAchievements.AddRangeAsync(newUnlocks);
    }
}
