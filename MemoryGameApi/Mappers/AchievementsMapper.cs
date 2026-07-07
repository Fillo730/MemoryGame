using MemoryGame_API.Dto;
using MemoryGame_API.IMappers;
using MemoryGame_API.Models;

namespace MemoryGame_API.Mappers;

public class AchievementsMapper : IAchievementsMapper
{
    public AchievementDto MapToAchievementDto(Achievement achievement, DateTime? unlockedAt)
    {
        var translation = achievement.Translations.FirstOrDefault();

        return new AchievementDto
        {
            Id = achievement.Id,
            Code = achievement.Code,
            Icon = achievement.Icon,
            Name = translation?.Name ?? "N/A",
            Description = translation?.Description ?? "N/A",
            Unlocked = unlockedAt.HasValue,
            UnlockedAt = unlockedAt,
        };
    }

    public IEnumerable<AchievementDto> MapToAchievementDtoList(IEnumerable<Achievement> achievements, IDictionary<int, DateTime> unlockedByAchievementId)
    {
        return achievements.Select(a => MapToAchievementDto(a, unlockedByAchievementId.TryGetValue(a.Id, out var unlockedAt) ? unlockedAt : null));
    }
}
