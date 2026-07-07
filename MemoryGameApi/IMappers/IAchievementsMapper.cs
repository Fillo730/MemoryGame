using MemoryGame_API.Dto;
using MemoryGame_API.Models;

namespace MemoryGame_API.IMappers;

public interface IAchievementsMapper
{
    AchievementDto MapToAchievementDto(Achievement achievement, DateTime? unlockedAt);

    IEnumerable<AchievementDto> MapToAchievementDtoList(IEnumerable<Achievement> achievements, IDictionary<int, DateTime> unlockedByAchievementId);
}
