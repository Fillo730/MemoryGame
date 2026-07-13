using MemoryGame_API.Dto;
using MemoryGame_API.IServices;
using MemoryGame_API.Utils;
using Microsoft.AspNetCore.Mvc;

namespace MemoryGame_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DifficultiesController (IDifficultiesService difficultiesService, ILogger<DifficultiesController> logger) : BaseController
{
    private readonly IDifficultiesService _difficultiesService = difficultiesService;

    private readonly ILogger<DifficultiesController> _logger = logger;

    [HttpGet]
    public async Task<IActionResult> GetAllDifficulties([FromQuery] string? lang)
    {
        try
        {
            var result = await _difficultiesService.GetDifficultiesAsync(GetLanguage(lang));

            return Ok(ApiResponse<IEnumerable<DifficultyDto>>.CreateSuccessResponse(result));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get difficulties list");
            return Ok(ApiResponse<string>.CreateFailureResponse(AppConstants.GENERIC_ERROR_MESSAGE));
        }
    }
}
