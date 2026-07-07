namespace MemoryGame_API.Models;

public class UserAchievement
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;

    public int AchievementId { get; set; }

    public virtual Achievement Achievement { get; set; } = null!;

    public DateTime UnlockedAt { get; set; } = DateTime.UtcNow;
}
