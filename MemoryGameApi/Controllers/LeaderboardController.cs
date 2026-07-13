using MemoryGame_API.Dto;
using MemoryGame_API.IServices;
using MemoryGame_API.Utils;
using Microsoft.AspNetCore.Mvc;

namespace MemoryGame_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LeaderboardController (ILeaderboardService leaderboardService, ILogger<LeaderboardController> logger) : BaseController
{
    private readonly ILeaderboardService _leaderboardService = leaderboardService;

    private readonly ILogger<LeaderboardController> _logger = logger;

    [HttpGet]
    public async Task<IActionResult> GetLeaderboard([FromQuery] string? lang)
    {
        try
        {
            var result = await _leaderboardService.GetLeaderboardAsync(GetLanguage(lang));

            return Ok(ApiResponse<LeaderboardDto>.CreateSuccessResponse(result));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get leaderboard");
            return Ok(ApiResponse<string>.CreateFailureResponse(AppConstants.GENERIC_ERROR_MESSAGE));
        }
    }

    [HttpGet("stats")]
    public async Task<IActionResult> GetPlatformStats([FromQuery] string? lang)
    {
        try
        {
            var result = await _leaderboardService.GetPlatformStatsAsync(GetLanguage(lang));

            return Ok(ApiResponse<PlatformStatsDto>.CreateSuccessResponse(result));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get platform stats");
            return Ok(ApiResponse<string>.CreateFailureResponse(AppConstants.GENERIC_ERROR_MESSAGE));
        }
    }
}
