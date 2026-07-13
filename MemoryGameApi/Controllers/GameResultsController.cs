using MemoryGame_API.Dto;
using MemoryGame_API.IServices;
using MemoryGame_API.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace MemoryGame_API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class GameResultsController (IGameResultsService gameResultsService, ILogger<GameResultsController> logger) : BaseController
{
    private readonly IGameResultsService _gameResultService = gameResultsService;

    private readonly ILogger<GameResultsController> _logger = logger;

    [HttpPost]
    public async Task<IActionResult> AddGameResultForUserByIdAsync([FromBody] GameResultDto gameResultDto)
    {
        try
        {
            if (gameResultDto.Difficulty is null)
            {
                return Ok(ApiResponse<string>.CreateFailureResponse("Difficulty not specified."));
            }

            if (gameResultDto.Moves <= 0 || gameResultDto.DurationSeconds < 0)
            {
                return Ok(ApiResponse<string>.CreateFailureResponse("Invalid moves or game duration."));
            }

            var userId = GetUserIdFromToken();

            if (userId <= 0)
            {
                return Ok(ApiResponse<string>.CreateFailureResponse("Invalid user."));
            }

            var result = await _gameResultService.AddGameResutlAsync(gameResultDto, userId);

            return Ok(ApiResponse<GameResultDto>.CreateSuccessResponse(result));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to add game result for user {UserId}", GetUserIdFromToken());

            return Ok(ApiResponse<string>.CreateFailureResponse(AppConstants.GENERIC_ERROR_MESSAGE));
        }
    }

    [Authorize]
    [HttpGet("history")]
    public async Task<IActionResult> GetGameHistoryByUserId([FromQuery] string? lang, [FromQuery] int page = 1, [FromQuery] int pageSize = 8)
    {
        try
        {
            var safePage = page < 1 ? 1 : page;
            var safePageSize = pageSize < 1 ? 8 : Math.Min(pageSize, 50);

            var result = await _gameResultService.GetGameHistoryForUserByIdAsync(GetUserIdFromToken(), GetLanguage(lang), safePage, safePageSize);

            return Ok(ApiResponse<PagedResultDto<GameResultDto>>.CreateSuccessResponse(result));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get game history for user {UserId}", GetUserIdFromToken());

            return Ok(ApiResponse<string>.CreateFailureResponse(AppConstants.GENERIC_ERROR_MESSAGE));
        }
    }

    [Authorize]
    [HttpGet("userStats")]
    public async Task<IActionResult> GetStatsByUserId([FromQuery] string? lang)
    {
        try
        {
            var result = await _gameResultService.GetUserStatsByIdAsync(GetUserIdFromToken(), GetLanguage(lang));

            return Ok(ApiResponse<IEnumerable<UserStatsDto>>.CreateSuccessResponse(result));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get user stats for user {UserId}", GetUserIdFromToken());
            
            return Ok(ApiResponse<string>.CreateFailureResponse(AppConstants.GENERIC_ERROR_MESSAGE));
        }
    }
}
