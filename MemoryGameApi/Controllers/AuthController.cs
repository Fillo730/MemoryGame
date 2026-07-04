using MemoryGame_API.Dto;
using MemoryGame_API.IServices;
using MemoryGame_API.Utils;
using Microsoft.AspNetCore.Mvc;

namespace MemoryGame_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController (IAuthService authService) : BaseController
{
    private readonly IAuthService _authService = authService;

    [HttpPost("login")]
    public async Task<IActionResult> LoginUserAsync([FromBody] LoginRequestDto loginRequest)
    {
        try
        {
            var result = await _authService.LoginUserAsync(loginRequest);

            if(result == null)
            {
                return Ok(ApiResponse<string>.CreateFailureResponse(""));
            }
            else
            {
                return Ok(ApiResponse<LoginResponseDto>.CreateSuccessResponse(result));
            }
        }
        catch(Exception ex)
        {
            return Ok(ApiResponse<string>.CreateFailureResponse(ex.Message));
        }
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUserAsync([FromBody] RegisterRequestDto registerRequest)
    {
        try
        {
            var result = await _authService.RegisterUserAsync(registerRequest);

            if (result == null)
            {
                return Ok(ApiResponse<string>.CreateFailureResponse(""));
            }
            else
            {
                return Ok(ApiResponse<LoginResponseDto>.CreateSuccessResponse(result));
            }
        }
        catch (Exception ex)
        {
            return Ok(ApiResponse<string>.CreateFailureResponse(ex.Message));
        }
    }
}
