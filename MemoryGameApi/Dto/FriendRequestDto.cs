namespace MemoryGame_API.Dto;

public class FriendRequestDto
{
    public int FriendshipId { get; set; }

    public int FromUserId { get; set; }

    public string FromUsername { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
}
