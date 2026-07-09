using MemoryGame_API.Dto;
using MemoryGame_API.IServices;
using MemoryGame_API.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MemoryGame_API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class FriendsController (IFriendsService friendsService) : BaseController
{
    private readonly IFriendsService _friendsService = friendsService;

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
            return Ok(ApiResponse<string>.CreateFailureResponse(ex.Message));
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
            return Ok(ApiResponse<string>.CreateFailureResponse(ex.Message));
        }
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchUsers([FromQuery] string? query)
    {
        try
        {
            var result = await _friendsService.SearchUsersAsync(GetUserIdFromToken(), query ?? string.Empty);

            return Ok(ApiResponse<IEnumerable<UserSearchResultDto>>.CreateSuccessResponse(result));
        }
        catch (Exception ex)
        {
            return Ok(ApiResponse<string>.CreateFailureResponse(ex.Message));
        }
    }

    [HttpPost("request/{userId}")]
    public async Task<IActionResult> SendFriendRequest(int userId)
    {
        try
        {
            var result = await _friendsService.SendFriendRequestAsync(GetUserIdFromToken(), userId);

            if (result is null)
            {
                return Ok(ApiResponse<string>.CreateFailureResponse("Impossibile inviare la richiesta di amicizia."));
            }

            return Ok(ApiResponse<FriendRequestDto>.CreateSuccessResponse(result));
        }
        catch (Exception ex)
        {
            return Ok(ApiResponse<string>.CreateFailureResponse(ex.Message));
        }
    }

    [HttpPost("{friendshipId}/accept")]
    public async Task<IActionResult> AcceptRequest(int friendshipId)
    {
        try
        {
            var success = await _friendsService.RespondToRequestAsync(friendshipId, GetUserIdFromToken(), true);

            if (!success)
            {
                return Ok(ApiResponse<string>.CreateFailureResponse("Richiesta non trovata."));
            }

            return Ok(ApiResponse<bool>.CreateSuccessResponse(true));
        }
        catch (Exception ex)
        {
            return Ok(ApiResponse<string>.CreateFailureResponse(ex.Message));
        }
    }

    [HttpPost("{friendshipId}/decline")]
    public async Task<IActionResult> DeclineRequest(int friendshipId)
    {
        try
        {
            var success = await _friendsService.RespondToRequestAsync(friendshipId, GetUserIdFromToken(), false);

            if (!success)
            {
                return Ok(ApiResponse<string>.CreateFailureResponse("Richiesta non trovata."));
            }

            return Ok(ApiResponse<bool>.CreateSuccessResponse(true));
        }
        catch (Exception ex)
        {
            return Ok(ApiResponse<string>.CreateFailureResponse(ex.Message));
        }
    }

    [HttpDelete("{friendshipId}")]
    public async Task<IActionResult> RemoveFriend(int friendshipId)
    {
        try
        {
            var success = await _friendsService.RemoveFriendAsync(friendshipId, GetUserIdFromToken());

            if (!success)
            {
                return Ok(ApiResponse<string>.CreateFailureResponse("Amicizia non trovata."));
            }

            return Ok(ApiResponse<bool>.CreateSuccessResponse(true));
        }
        catch (Exception ex)
        {
            return Ok(ApiResponse<string>.CreateFailureResponse(ex.Message));
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
            return Ok(ApiResponse<string>.CreateFailureResponse(ex.Message));
        }
    }

    [HttpGet("{userId}/profile")]
    public async Task<IActionResult> GetFriendProfile(int userId, [FromQuery] string? lang, [FromQuery] int historyPage = 1, [FromQuery] int historyPageSize = 8)
    {
        try
        {
            var result = await _friendsService.GetFriendProfileAsync(GetUserIdFromToken(), userId, GetLanguage(lang), historyPage, historyPageSize);

            if (result is null)
            {
                return Ok(ApiResponse<string>.CreateFailureResponse("Potete vedere solo il profilo dei vostri amici."));
            }

            return Ok(ApiResponse<FriendProfileDto>.CreateSuccessResponse(result));
        }
        catch (Exception ex)
        {
            return Ok(ApiResponse<string>.CreateFailureResponse(ex.Message));
        }
    }
}
