using MemoryGame_API.Dto;
using MemoryGame_API.IMappers;
using MemoryGame_API.IRepositories;
using MemoryGame_API.IServices;
using MemoryGame_API.Models;
using MemoryGame_API.Utils;

namespace MemoryGame_API.Services;

public class FriendsService (
    IFriendshipRepository friendshipRepository,
    IUserRepository userRepository,
    IFriendsMapper friendsMapper,
    IAchievementsService achievementsService,
    IGameResultsService gameResultsService) : IFriendsService
{
    private readonly IFriendshipRepository _friendshipRepository = friendshipRepository;

    private readonly IUserRepository _userRepository = userRepository;

    private readonly IFriendsMapper _friendsMapper = friendsMapper;

    private readonly IAchievementsService _achievementsService = achievementsService;

    private readonly IGameResultsService _gameResultsService = gameResultsService;

    public async Task<FriendRequestDto?> SendFriendRequestAsync(int requesterId, int targetUserId)
    {
        if (targetUserId == requesterId)
        {
            return null;
        }

        var target = await _userRepository.GetUserByIdAsync(targetUserId);

        if (target is null)
        {
            return null;
        }

        var existing = await _friendshipRepository.GetBetweenUsersAsync(requesterId, target.Id);

        if (existing is not null)
        {
            return null;
        }

        var requester = await _userRepository.GetUserByIdAsync(requesterId);

        if (requester is null)
        {
            return null;
        }

        var friendship = new Friendship
        {
            RequesterId = requesterId,
            AddresseeId = target.Id,
            Status = FriendshipStatusEnum.Pending
        };

        await _friendshipRepository.AddAsync(friendship);
        await _friendshipRepository.SaveChangesAsync();

        friendship.Requester = requester;

        return _friendsMapper.MapToFriendRequestDto(friendship);
    }

    public async Task<bool> RespondToRequestAsync(int friendshipId, int callerId, bool accept)
    {
        var friendship = await _friendshipRepository.GetByIdAsync(friendshipId);

        if (friendship is null || friendship.AddresseeId != callerId || friendship.Status != FriendshipStatusEnum.Pending)
        {
            return false;
        }

        friendship.Status = accept ? FriendshipStatusEnum.Accepted : FriendshipStatusEnum.Declined;
        friendship.RespondedAt = DateTime.UtcNow;

        await _friendshipRepository.SaveChangesAsync();

        return true;
    }

    public async Task<bool> RemoveFriendAsync(int friendshipId, int callerId)
    {
        var friendship = await _friendshipRepository.GetByIdAsync(friendshipId);

        if (friendship is null || (friendship.RequesterId != callerId && friendship.AddresseeId != callerId))
        {
            return false;
        }

        _friendshipRepository.Remove(friendship);
        await _friendshipRepository.SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<FriendDto>> GetFriendsAsync(int userId)
    {
        var friendships = await _friendshipRepository.GetAcceptedFriendshipsAsync(userId);

        return friendships.Select(f => _friendsMapper.MapToFriendDto(f.RequesterId == userId ? f.Addressee : f.Requester, f.Id));
    }

    public async Task<IEnumerable<FriendRequestDto>> GetIncomingRequestsAsync(int userId)
    {
        var requests = await _friendshipRepository.GetIncomingPendingRequestsAsync(userId);

        return requests.Select(_friendsMapper.MapToFriendRequestDto);
    }

    public async Task<IEnumerable<UserSearchResultDto>> SearchUsersAsync(int callerId, string query)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            return new List<UserSearchResultDto>();
        }

        var users = await _userRepository.SearchUsersByUsernameAsync(query, callerId, 20);
        var results = new List<UserSearchResultDto>();

        foreach (var user in users)
        {
            var friendship = await _friendshipRepository.GetBetweenUsersAsync(callerId, user.Id);

            var status = friendship?.Status switch
            {
                FriendshipStatusEnum.Accepted => "Friends",
                FriendshipStatusEnum.Pending when friendship!.RequesterId == callerId => "PendingOutgoing",
                FriendshipStatusEnum.Pending => "PendingIncoming",
                FriendshipStatusEnum.Declined => "Declined",
                _ => "None"
            };

            results.Add(_friendsMapper.MapToUserSearchResultDto(user, status));
        }

        return results;
    }

    public async Task<FriendProfileDto?> GetFriendProfileAsync(int callerId, int targetUserId, string lang, int historyPage, int historyPageSize)
    {
        var friendship = await _friendshipRepository.GetBetweenUsersAsync(callerId, targetUserId);

        if (friendship is null || friendship.Status != FriendshipStatusEnum.Accepted)
        {
            return null;
        }

        var targetUser = await _userRepository.GetUserByIdAsync(targetUserId);

        if (targetUser is null)
        {
            return null;
        }

        var achievements = await _achievementsService.GetAchievementsForUserAsync(targetUserId, lang);
        var stats = await _gameResultsService.GetUserStatsByIdAsync(targetUserId, lang);
        var history = await _gameResultsService.GetGameHistoryForUserByIdAsync(targetUserId, lang, historyPage, historyPageSize);

        return new FriendProfileDto
        {
            UserId = targetUser.Id,
            Username = targetUser.Username,
            Achievements = achievements,
            Stats = stats,
            History = history
        };
    }

    public async Task<IEnumerable<FriendComparisonEntryDto>> GetFriendsComparisonAsync(int userId, string lang)
    {
        var currentUser = await _userRepository.GetUserByIdAsync(userId);

        if (currentUser is null)
        {
            return new List<FriendComparisonEntryDto>();
        }

        var friends = await GetFriendsAsync(userId);

        var entries = new List<FriendComparisonEntryDto>
        {
            await BuildComparisonEntryAsync(userId, currentUser.Username, lang, isCurrentUser: true)
        };

        foreach (var friend in friends)
        {
            entries.Add(await BuildComparisonEntryAsync(friend.UserId, friend.Username, lang, isCurrentUser: false));
        }

        return entries.OrderByDescending(e => e.TotalGamesPlayed);
    }

    private async Task<FriendComparisonEntryDto> BuildComparisonEntryAsync(int targetUserId, string username, string lang, bool isCurrentUser)
    {
        var stats = (await _gameResultsService.GetUserStatsByIdAsync(targetUserId, lang)).ToList();
        var playedStats = stats.Where(s => s.GamesPlayed > 0).ToList();

        return new FriendComparisonEntryDto
        {
            UserId = targetUserId,
            Username = username,
            TotalGamesPlayed = stats.Sum(s => s.GamesPlayed),
            BestOverallScore = playedStats.Count == 0 ? 0 : playedStats.Min(s => s.BestScore),
            IsCurrentUser = isCurrentUser
        };
    }
}
