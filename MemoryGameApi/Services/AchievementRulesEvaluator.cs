using MemoryGame_API.IServices;
using MemoryGame_API.Models;
using MemoryGame_API.Utils;

namespace MemoryGame_API.Services;

public class AchievementRulesEvaluator : IAchievementRulesEvaluator
{
    public HashSet<string> EvaluateUnlockedCodes(IEnumerable<GameResult> gameResults, int totalDifficultyCount, int hardestDifficultyId)
    {
        var results = gameResults.ToList();
        var unlocked = new HashSet<string>();
        var totalGames = results.Count;

        if (totalGames >= 1) unlocked.Add(AchievementCodes.FirstWin);
        if (totalGames >= 10) unlocked.Add(AchievementCodes.TenGames);
        if (totalGames >= 50) unlocked.Add(AchievementCodes.FiftyGames);
        if (totalGames >= 100) unlocked.Add(AchievementCodes.HundredGames);

        if (results.Any(r => r.Moves == r.Difficulty.NumberOfPairs))
            unlocked.Add(AchievementCodes.Flawless);

        if (totalDifficultyCount > 0 && results.Select(r => r.DifficultyId).Distinct().Count() >= totalDifficultyCount)
            unlocked.Add(AchievementCodes.AllDifficulties);

        if (results.Any(r => r.DifficultyId == hardestDifficultyId))
            unlocked.Add(AchievementCodes.HardestDifficulty);

        if (results.Any(r => r.PlayedAt.Hour >= 0 && r.PlayedAt.Hour < 5))
            unlocked.Add(AchievementCodes.NightOwl);

        if (totalGames >= 500) unlocked.Add(AchievementCodes.FiveHundredGames);

        if (results.Any(r => r.PlayedAt.Hour >= 5 && r.PlayedAt.Hour < 8))
            unlocked.Add(AchievementCodes.EarlyBird);

        if (results.Any(r => r.PlayedAt.DayOfWeek == DayOfWeek.Saturday || r.PlayedAt.DayOfWeek == DayOfWeek.Sunday))
            unlocked.Add(AchievementCodes.WeekendWarrior);

        if (results.Count(r => r.Moves == r.Difficulty.NumberOfPairs) >= 3)
            unlocked.Add(AchievementCodes.PerfectStreak);

        if (results.GroupBy(r => r.PlayedAt.Date).Any(g => g.Count() >= 5))
            unlocked.Add(AchievementCodes.MarathonDay);

        if (results.Any(r => r.DifficultyId == hardestDifficultyId && r.Moves == r.Difficulty.NumberOfPairs))
            unlocked.Add(AchievementCodes.FlawlessLegend);

        return unlocked;
    }
}
