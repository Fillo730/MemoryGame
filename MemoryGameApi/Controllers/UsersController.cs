using MemoryGame_API.Dto;
using MemoryGame_API.IServices;
using MemoryGame_API.Utils;
using Microsoft.AspNetCore.Mvc;

namespace MemoryGame_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController (IUsersService usersService, ILogger<UsersController> logger) : BaseController
{
    private readonly IUsersService _usersService = usersService;

    private readonly ILogger<UsersController> _logger = logger;

    [HttpPut]
    public async Task<IActionResult> UpdateUserAsync([FromBody] UpdateRequestDto updateRequestDto)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(updateRequestDto.Username) || updateRequestDto.Username.Trim().Length < 3)
            {
                return Ok(ApiResponse<string>.CreateFailureResponse("Username must be at least 3 characters long."));
            }

            if (string.IsNullOrWhiteSpace(updateRequestDto.Email) || !updateRequestDto.Email.Contains('@'))
            {
                return Ok(ApiResponse<string>.CreateFailureResponse("Invalid email."));
            }

            var userId = GetUserIdFromToken();

            if (userId <= 0)
            {
                return Ok(ApiResponse<string>.CreateFailureResponse("Invalid user."));
            }

            var result = await _usersService.UpdateProfileAsync(updateRequestDto, userId);

            if (result == null)
            {
                return Ok(ApiResponse<string>.CreateFailureResponse("Unable to update profile. Username or email may already be in use."));
            }

            return Ok(ApiResponse<LoginResponseDto>.CreateSuccessResponse(result));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to update profile for user {UserId}", GetUserIdFromToken());
            return Ok(ApiResponse<string>.CreateFailureResponse(AppConstants.GENERIC_ERROR_MESSAGE));
        }
    }
}
