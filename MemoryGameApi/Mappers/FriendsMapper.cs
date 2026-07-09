using MemoryGame_API.Dto;
using MemoryGame_API.IMappers;
using MemoryGame_API.Models;

namespace MemoryGame_API.Mappers;

public class FriendsMapper : IFriendsMapper
{
    public FriendDto MapToFriendDto(User otherUser, int friendshipId)
    {
        return new FriendDto
        {
            FriendshipId = friendshipId,
            UserId = otherUser.Id,
            Username = otherUser.Username
        };
    }

    public FriendRequestDto MapToFriendRequestDto(Friendship friendship)
    {
        return new FriendRequestDto
        {
            FriendshipId = friendship.Id,
            FromUserId = friendship.RequesterId,
            FromUsername = friendship.Requester.Username,
            CreatedAt = friendship.CreatedAt
        };
    }

    public UserSearchResultDto MapToUserSearchResultDto(User user, string status)
    {
        return new UserSearchResultDto
        {
            UserId = user.Id,
            Username = user.Username,
            Status = status
        };
    }
}
