using MemoryGame_API.Utils;

namespace MemoryGame_API.Models;

public class Friendship
{
    public int Id { get; set; }

    public int RequesterId { get; set; }

    public virtual User Requester { get; set; } = null!;

    public int AddresseeId { get; set; }

    public virtual User Addressee { get; set; } = null!;

    public FriendshipStatusEnum Status { get; set; } = FriendshipStatusEnum.Pending;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? RespondedAt { get; set; }
}
