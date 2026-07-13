using MemoryGame_API.Dto;
using MemoryGame_API.IServices;
using MemoryGame_API.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MemoryGame_API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class FriendsController (IFriendsService friendsService, ILogger<FriendsController> logger) : BaseController
{
    private readonly IFriendsService _friendsService = friendsService;

    private readonly ILogger<FriendsController> _logger = logger;

    [HttpGet]
    public async Task<IActionResult> GetFriends()
    {
        try
        {
            var result = await _friendsService.GetFriendsAsync(GetUserIdFromToken());

            return Ok(ApiResponse<IEnumerable<FriendDto>>.CreateSuccessResponse(result));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get friends list for user {UserId}", GetUserIdFromToken());
            return Ok(ApiResponse<string>.CreateFailureResponse(AppConstants.GENERIC_ERROR_MESSAGE));
        }
    }

    [HttpGet("requests")]
    public async Task<IActionResult> GetIncomingRequests()
    {
        try
        {
            var result = await _friendsService.GetIncomingRequestsAsync(GetUserIdFromToken());

            return Ok(ApiResponse<IEnumerable<FriendRequestDto>>.CreateSuccessResponse(result));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get incoming friend requests for user {UserId}", GetUserIdFromToken());
            return Ok(ApiResponse<string>.CreateFailureResponse(AppConstants.GENERIC_ERROR_MESSAGE));
        }
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchUsers([FromQuery] string? query)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(query) || query.Trim().Length < 2)
            {
                return Ok(ApiResponse<IEnumerable<UserSearchResultDto>>.CreateSuccessResponse([]));
            }

            var result = await _friendsService.SearchUsersAsync(GetUserIdFromToken(), query.Trim());

            return Ok(ApiResponse<IEnumerable<UserSearchResultDto>>.CreateSuccessResponse(result));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to search users with query {Query}", query);
            return Ok(ApiResponse<string>.CreateFailureResponse(AppConstants.GENERIC_ERROR_MESSAGE));
        }
    }

    [HttpPost("request/{userId}")]
    public async Task<IActionResult> SendFriendRequest(int userId)
    {
        try
        {
            if (userId <= 0)
            {
                return Ok(ApiResponse<string>.CreateFailureResponse("Invalid user id."));
            }

            var result = await _friendsService.SendFriendRequestAsync(GetUserIdFromToken(), userId);

            if (result is null)
            {
                return Ok(ApiResponse<string>.CreateFailureResponse("Unable to send friend request."));
            }

            return Ok(ApiResponse<FriendRequestDto>.CreateSuccessResponse(result));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send friend request from user {UserId} to {TargetUserId}", GetUserIdFromToken(), userId);
            return Ok(ApiResponse<string>.CreateFailureResponse(AppConstants.GENERIC_ERROR_MESSAGE));
        }
    }

    [HttpPost("{friendshipId}/accept")]
    public async Task<IActionResult> AcceptRequest(int friendshipId)
    {
        try
        {
            if (friendshipId <= 0)
            {
                return Ok(ApiResponse<string>.CreateFailureResponse("Invalid request id."));
            }

            var success = await _friendsService.RespondToRequestAsync(friendshipId, GetUserIdFromToken(), true);

            if (!success)
            {
                return Ok(ApiResponse<string>.CreateFailureResponse("Request not found."));
            }

            return Ok(ApiResponse<bool>.CreateSuccessResponse(true));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to accept friend request {FriendshipId}", friendshipId);
            return Ok(ApiResponse<string>.CreateFailureResponse(AppConstants.GENERIC_ERROR_MESSAGE));
        }
    }

    [HttpPost("{friendshipId}/decline")]
    public async Task<IActionResult> DeclineRequest(int friendshipId)
    {
        try
        {
            if (friendshipId <= 0)
            {
                return Ok(ApiResponse<string>.CreateFailureResponse("Invalid request id."));
            }

            var success = await _friendsService.RespondToRequestAsync(friendshipId, GetUserIdFromToken(), false);

            if (!success)
            {
                return Ok(ApiResponse<string>.CreateFailureResponse("Request not found."));
            }

            return Ok(ApiResponse<bool>.CreateSuccessResponse(true));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to decline friend request {FriendshipId}", friendshipId);
            return Ok(ApiResponse<string>.CreateFailureResponse(AppConstants.GENERIC_ERROR_MESSAGE));
        }
    }

    [HttpDelete("{friendshipId}")]
    public async Task<IActionResult> RemoveFriend(int friendshipId)
    {
        try
        {
            if (friendshipId <= 0)
            {
                return Ok(ApiResponse<string>.CreateFailureResponse("Invalid friendship id."));
            }

            var success = await _friendsService.RemoveFriendAsync(friendshipId, GetUserIdFromToken());

            if (!success)
            {
                return Ok(ApiResponse<string>.CreateFailureResponse("Friendship not found."));
            }

            return Ok(ApiResponse<bool>.CreateSuccessResponse(true));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to remove friendship {FriendshipId}", friendshipId);
            return Ok(ApiResponse<string>.CreateFailureResponse(AppConstants.GENERIC_ERROR_MESSAGE));
        }
    }

    [HttpGet("comparison")]
    public async Task<IActionResult> GetFriendsComparison([FromQuery] string? lang)
    {
        try
        {
            var result = await _friendsService.GetFriendsComparisonAsync(GetUserIdFromToken(), GetLanguage(lang));

            return Ok(ApiResponse<IEnumerable<FriendComparisonEntryDto>>.CreateSuccessResponse(result));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get friends comparison for user {UserId}", GetUserIdFromToken());
            return Ok(ApiResponse<string>.CreateFailureResponse(AppConstants.GENERIC_ERROR_MESSAGE));
        }
    }

    [HttpGet("{userId}/profile")]
    public async Task<IActionResult> GetFriendProfile(int userId, [FromQuery] string? lang, [FromQuery] int historyPage = 1, [FromQuery] int historyPageSize = 8)
    {
        try
        {
            if (userId <= 0)
            {
                return Ok(ApiResponse<string>.CreateFailureResponse("Invalid user id."));
            }

            var safePage = historyPage < 1 ? 1 : historyPage;
            var safePageSize = historyPageSize < 1 ? 8 : Math.Min(historyPageSize, 50);

            var result = await _friendsService.GetFriendProfileAsync(GetUserIdFromToken(), userId, GetLanguage(lang), safePage, safePageSize);

            if (result is null)
            {
                return Ok(ApiResponse<string>.CreateFailureResponse("You can only view the profile of your friends."));
            }

            return Ok(ApiResponse<FriendProfileDto>.CreateSuccessResponse(result));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get friend profile {TargetUserId} for user {UserId}", userId, GetUserIdFromToken());
            return Ok(ApiResponse<string>.CreateFailureResponse(AppConstants.GENERIC_ERROR_MESSAGE));
        }
    }
}
