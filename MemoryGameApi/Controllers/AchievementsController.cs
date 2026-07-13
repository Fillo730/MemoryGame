using MemoryGame_API.Dto;
using MemoryGame_API.IServices;
using MemoryGame_API.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MemoryGame_API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class AchievementsController (IAchievementsService achievementsService, ILogger<AchievementsController> logger) : BaseController
{
    private readonly IAchievementsService _achievementsService = achievementsService;

    private readonly ILogger<AchievementsController> _logger = logger;

    [HttpGet]
    public async Task<IActionResult> GetAchievements([FromQuery] string? lang)
    {
        try
        {
            var result = await _achievementsService.GetAchievementsForUserAsync(GetUserIdFromToken(), GetLanguage(lang));

            return Ok(ApiResponse<IEnumerable<AchievementDto>>.CreateSuccessResponse(result));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get achievements for user {UserId}", GetUserIdFromToken());
            
            return Ok(ApiResponse<string>.CreateFailureResponse(AppConstants.GENERIC_ERROR_MESSAGE));
        }
    }
}
