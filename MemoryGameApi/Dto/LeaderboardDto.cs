namespace MemoryGame_API.Dto;

public class TopPlayerDto
{
    public string Username { get; set; } = string.Empty;

    public int GamesCompleted { get; set; }
}

public class DifficultyGamesCountDto
{
    public DifficultyDto Difficulty { get; set; } = null!;

    public int GamesPlayed { get; set; }
}

public class BestScoreEntryDto
{
    public string Username { get; set; } = string.Empty;

    public int Moves { get; set; }
}

public class DifficultyBestScoresDto
{
    public DifficultyDto Difficulty { get; set; } = null!;

    public IEnumerable<BestScoreEntryDto> TopScores { get; set; } = new List<BestScoreEntryDto>();
}

public class LeaderboardDto
{
    public IEnumerable<TopPlayerDto> TopPlayers { get; set; } = new List<TopPlayerDto>();

    public IEnumerable<DifficultyGamesCountDto> GamesPerDifficulty { get; set; } = new List<DifficultyGamesCountDto>();

    public IEnumerable<DifficultyBestScoresDto> BestScoresPerDifficulty { get; set; } = new List<DifficultyBestScoresDto>();
}
