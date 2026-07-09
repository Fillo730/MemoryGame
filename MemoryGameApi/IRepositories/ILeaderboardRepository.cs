using MemoryGame_API.Models;

namespace MemoryGame_API.IRepositories;

public interface ILeaderboardRepository
{
    Task<IEnumerable<TopPlayer>> GetTopPlayersAsync(int topN);

    Task<IEnumerable<DifficultyGamesCount>> GetGamesPerDifficultyAsync(string lang);

    Task<IEnumerable<DifficultyBestScores>> GetBestScoresPerDifficultyAsync(int topN, string lang);

    Task<IEnumerable<DifficultyBestTimes>> GetBestTimesPerDifficultyAsync(int topN, string lang);

    Task<PlatformStats> GetPlatformStatsAsync(string lang);
}
