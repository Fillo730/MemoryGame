using MemoryGame_API.Dto;
using MemoryGame_API.Models;

namespace MemoryGame_API.IMappers;

public interface IFriendsMapper
{
    FriendDto MapToFriendDto(User otherUser, int friendshipId);

    FriendRequestDto MapToFriendRequestDto(Friendship friendship);

    UserSearchResultDto MapToUserSearchResultDto(User user, string status);
}
