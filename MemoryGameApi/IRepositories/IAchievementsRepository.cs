using MemoryGame_API.Models;

namespace MemoryGame_API.IRepositories;

public interface IAchievementsRepository
{
    Task<IEnumerable<Achievement>> GetAllAsync();

    Task<IEnumerable<Achievement>> GetAllWithTranslationsAsync(string lang);

    Task<IEnumerable<Difficulty>> GetDifficultyBenchmarksAsync();

    Task<IEnumerable<UserAchievement>> GetUnlockedForUserAsync(int userId);

    Task AddUnlockedAsync(int userId, IEnumerable<int> achievementIds);
}
