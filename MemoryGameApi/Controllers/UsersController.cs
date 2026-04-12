using MemoryGame_API.Dto;
using MemoryGame_API.IServices;
using MemoryGame_API.Utils;
using Microsoft.AspNetCore.Mvc;

namespace MemoryGame_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController (IUsersService usersService) : BaseController
{
    private readonly IUsersService _usersService = usersService;
    [HttpPut]
    public async Task<IActionResult> UpdateUserAsync([FromBody] UpdateRequestDto updateRequestDto)
    {
        try
        {
            var result = await _usersService.UpdateProfileAsync(updateRequestDto, GetUserIdFromToken());

            if (result == null)
            {
                return Ok(ApiResponse<string>.CreateFailureResponse(""));
            }

            return Ok(ApiResponse<LoginResponseDto>.CreateSuccessResponse(result));
        }
        catch (Exception ex)
        {
            return Ok(ApiResponse<string>.CreateFailureResponse(ex.Message));
        }
    }
}
