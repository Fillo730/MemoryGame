namespace MemoryGame_API.Models;

public class DifficultyTranslation
{
    public int Id { get; set; }

    public int DifficultyId { get; set; }

    public Difficulty Difficulty { get; set; } = null!;

    public string LanguageCode { get; set; } = string.Empty;

    public string Label { get; set; } = string.Empty;
}
