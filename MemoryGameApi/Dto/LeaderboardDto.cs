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

public class BestTimeEntryDto
{
    public string Username { get; set; } = string.Empty;

    public int DurationSeconds { get; set; }
}

public class DifficultyBestTimesDto
{
    public DifficultyDto Difficulty { get; set; } = null!;

    public IEnumerable<BestTimeEntryDto> TopTimes { get; set; } = new List<BestTimeEntryDto>();
}

public class PlatformStatsDto
{
    public int TotalPlayers { get; set; }

    public int TotalGamesPlayed { get; set; }

    public DifficultyDto? MostPopularDifficulty { get; set; }
}

public class LeaderboardDto
{
    public IEnumerable<TopPlayerDto> TopPlayers { get; set; } = new List<TopPlayerDto>();

    public IEnumerable<DifficultyGamesCountDto> GamesPerDifficulty { get; set; } = new List<DifficultyGamesCountDto>();

    public IEnumerable<DifficultyBestScoresDto> BestScoresPerDifficulty { get; set; } = new List<DifficultyBestScoresDto>();

    public IEnumerable<DifficultyBestTimesDto> BestTimesPerDifficulty { get; set; } = new List<DifficultyBestTimesDto>();
}
