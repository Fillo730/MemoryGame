namespace MemoryGame_API.Dto;

public class FriendProfileDto
{
    public int UserId { get; set; }

    public string Username { get; set; } = string.Empty;

    public IEnumerable<AchievementDto> Achievements { get; set; } = new List<AchievementDto>();

    public IEnumerable<UserStatsDto> Stats { get; set; } = new List<UserStatsDto>();

    public PagedResultDto<GameResultDto> History { get; set; } = new();
}
