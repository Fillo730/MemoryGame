namespace MemoryGame_API.Models;

public class UserStats
{
    public Difficulty Difficulty { get; set; } = null!;

    public int GamesPlayed { get; set; }

    public int TotalMoves { get; set; }

    public int BestScore { get; set; }
}
