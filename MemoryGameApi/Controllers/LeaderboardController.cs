using MemoryGame_API.Dto;
using MemoryGame_API.IServices;
using MemoryGame_API.Utils;
using Microsoft.AspNetCore.Mvc;

namespace MemoryGame_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LeaderboardController (ILeaderboardService leaderboardService) : BaseController
{
    private readonly ILeaderboardService _leaderboardService = leaderboardService;

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
            return Ok(ApiResponse<string>.CreateFailureResponse(ex.Message));
        }
    }
}
