namespace MemoryGame_API.Models;

public class AchievementTranslation
{
    public int Id { get; set; }

    public int AchievementId { get; set; }

    public Achievement Achievement { get; set; } = null!;

    public string LanguageCode { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
}
