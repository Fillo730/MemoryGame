using MemoryGame_API.Dto;
using MemoryGame_API.IServices;
using MemoryGame_API.Utils;
using Microsoft.AspNetCore.Mvc;

namespace MemoryGame_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DifficultiesController (IDifficultiesService difficultiesService) : BaseController
{
    private readonly IDifficultiesService _difficultiesService = difficultiesService;

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
            return Ok(ApiResponse<string>.CreateFailureResponse(ex.Message));
        }
    }
}
