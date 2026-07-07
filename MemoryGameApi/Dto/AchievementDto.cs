namespace MemoryGame_API.Dto;

public class AchievementDto
{
    public int Id { get; set; }

    public string Code { get; set; } = string.Empty;

    public string Icon { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public bool Unlocked { get; set; }

    public DateTime? UnlockedAt { get; set; }
}
