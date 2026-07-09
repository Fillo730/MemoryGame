namespace MemoryGame_API.Dto;

public class FriendDto
{
    public int FriendshipId { get; set; }

    public int UserId { get; set; }

    public string Username { get; set; } = string.Empty;
}

public class FriendComparisonEntryDto
{
    public int UserId { get; set; }

    public string Username { get; set; } = string.Empty;

    public int TotalGamesPlayed { get; set; }

    public int BestOverallScore { get; set; }

    public bool IsCurrentUser { get; set; }
}
