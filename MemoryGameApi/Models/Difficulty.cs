namespace MemoryGame_API.Models;
public class Difficulty
{
    public int Id { get; set; }

    public int NumberOfPairs { get; set; }

    public ICollection<DifficultyTranslation> Translations { get; set; } = new List<DifficultyTranslation>();

    public virtual ICollection<GameResult> GameResults { get; set; } = new List<GameResult>();
}