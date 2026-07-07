namespace MemoryGame_API.Models;

public class Achievement
{
    public int Id { get; set; }

    public string Code { get; set; } = string.Empty;

    public string Icon { get; set; } = string.Empty;

    public ICollection<AchievementTranslation> Translations { get; set; } = new List<AchievementTranslation>();

    public virtual ICollection<UserAchievement> UserAchievements { get; set; } = new List<UserAchievement>();
}
