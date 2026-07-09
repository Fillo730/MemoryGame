using MemoryGame_API.Dto;

namespace MemoryGame_API.IServices;

public interface IFriendsService
{
    Task<FriendRequestDto?> SendFriendRequestAsync(int requesterId, int targetUserId);

    Task<bool> RespondToRequestAsync(int friendshipId, int callerId, bool accept);

    Task<bool> RemoveFriendAsync(int friendshipId, int callerId);

    Task<IEnumerable<FriendDto>> GetFriendsAsync(int userId);

    Task<IEnumerable<FriendRequestDto>> GetIncomingRequestsAsync(int userId);

    Task<IEnumerable<UserSearchResultDto>> SearchUsersAsync(int callerId, string query);

    Task<FriendProfileDto?> GetFriendProfileAsync(int callerId, int targetUserId, string lang, int historyPage, int historyPageSize);

    Task<IEnumerable<FriendComparisonEntryDto>> GetFriendsComparisonAsync(int userId, string lang);
}
