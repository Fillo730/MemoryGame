using MemoryGame_API.Dto;

namespace MemoryGame_API.IServices;

public interface IAchievementsService
{
    Task<IEnumerable<AchievementDto>> GetAchievementsForUserAsync(int userId, string lang);

    Task EvaluateAndUnlockNewAsync(int userId);
}
