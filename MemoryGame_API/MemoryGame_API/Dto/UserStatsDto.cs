namespace MemoryGame_API.Dto;

public class UserStatsDto
{
    public DifficultyDto Difficulty { get; set; } = null!;

    public int GamesPlayed { get; set; }

    public int TotalMoves { get; set; }

    public int BestScore { get; set; }

    public double AverageMovesPerGame { get; set; }
}
