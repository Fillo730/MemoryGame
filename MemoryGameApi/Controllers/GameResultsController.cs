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
public class GameResultsController (IGameResultsService gameResultsService) : BaseController
{
    private readonly IGameResultsService _gameResultService = gameResultsService;

    [HttpPost]
    public async Task<IActionResult> AddGameResultForUserByIdAsync([FromBody] GameResultDto gameResultDto)
    {
        try
        {
            var result = await _gameResultService.AddGameResutlAsync(gameResultDto, GetUserIdFromToken());

            return Ok(ApiResponse<GameResultDto>.CreateSuccessResponse(result));
        }
        catch (Exception ex)
        {
            return Ok(ApiResponse<string>.CreateFailureResponse(ex.Message));
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
            return Ok(ApiResponse<string>.CreateSuccessResponse(ex.Message));
        }
    }
}
