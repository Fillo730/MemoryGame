using MemoryGame_API.Models;

namespace MemoryGame_API.IServices;

public interface IAchievementRulesEvaluator
{
    HashSet<string> EvaluateUnlockedCodes(IEnumerable<GameResult> gameResults, int totalDifficultyCount, int hardestDifficultyId);
}
