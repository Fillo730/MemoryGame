namespace MemoryGame_API.Models;

public class TopPlayer
{
    public string Username { get; set; } = string.Empty;

    public int GamesCompleted { get; set; }
}

public class DifficultyGamesCount
{
    public Difficulty Difficulty { get; set; } = null!;

    public int GamesPlayed { get; set; }
}

public class BestScoreEntry
{
    public string Username { get; set; } = string.Empty;

    public int Moves { get; set; }
}

public class DifficultyBestScores
{
    public Difficulty Difficulty { get; set; } = null!;

    public IEnumerable<BestScoreEntry> TopScores { get; set; } = new List<BestScoreEntry>();
}

public class BestTimeEntry
{
    public string Username { get; set; } = string.Empty;

    public int DurationSeconds { get; set; }
}

public class DifficultyBestTimes
{
    public Difficulty Difficulty { get; set; } = null!;

    public IEnumerable<BestTimeEntry> TopTimes { get; set; } = new List<BestTimeEntry>();
}

public class PlatformStats
{
    public int TotalPlayers { get; set; }

    public int TotalGamesPlayed { get; set; }

    public Difficulty? MostPopularDifficulty { get; set; }
}

public class Leaderboard
{
    public IEnumerable<TopPlayer> TopPlayers { get; set; } = new List<TopPlayer>();

    public IEnumerable<DifficultyGamesCount> GamesPerDifficulty { get; set; } = new List<DifficultyGamesCount>();

    public IEnumerable<DifficultyBestScores> BestScoresPerDifficulty { get; set; } = new List<DifficultyBestScores>();

    public IEnumerable<DifficultyBestTimes> BestTimesPerDifficulty { get; set; } = new List<DifficultyBestTimes>();
}
