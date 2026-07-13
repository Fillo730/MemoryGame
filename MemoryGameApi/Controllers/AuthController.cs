using MemoryGame_API.Dto;
using MemoryGame_API.IServices;
using MemoryGame_API.Utils;
using Microsoft.AspNetCore.Mvc;

namespace MemoryGame_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController (IAuthService authService, ILogger<AuthController> logger) : BaseController
{
    private readonly IAuthService _authService = authService;

    private readonly ILogger<AuthController> _logger = logger;

    [HttpPost("login")]
    public async Task<IActionResult> LoginUserAsync([FromBody] LoginRequestDto loginRequest)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(loginRequest.Username) || string.IsNullOrWhiteSpace(loginRequest.Password))
            {
                return Ok(ApiResponse<string>.CreateFailureResponse("Username and password are required."));
            }

            var result = await _authService.LoginUserAsync(loginRequest);

            if(result == null)
            {
                return Ok(ApiResponse<string>.CreateFailureResponse("Invalid username or password."));
            }
            else
            {
                return Ok(ApiResponse<LoginResponseDto>.CreateSuccessResponse(result));
            }
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Login failed for user {Username}", loginRequest.Username);
            
            return Ok(ApiResponse<string>.CreateFailureResponse(AppConstants.GENERIC_ERROR_MESSAGE));
        }
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUserAsync([FromBody] RegisterRequestDto registerRequest)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(registerRequest.Username) || registerRequest.Username.Trim().Length < 3)
            {
                return Ok(ApiResponse<string>.CreateFailureResponse("Username must be at least 3 characters long."));
            }

            if (string.IsNullOrWhiteSpace(registerRequest.Email) || !registerRequest.Email.Contains('@'))
            {
                return Ok(ApiResponse<string>.CreateFailureResponse("Invalid email."));
            }

            if (string.IsNullOrWhiteSpace(registerRequest.Password) || registerRequest.Password.Length < 6)
            {
                return Ok(ApiResponse<string>.CreateFailureResponse("Password must be at least 6 characters long."));
            }

            var result = await _authService.RegisterUserAsync(registerRequest);

            if (result == null)
            {
                return Ok(ApiResponse<string>.CreateFailureResponse("Username or email already in use."));
            }
            else
            {
                return Ok(ApiResponse<LoginResponseDto>.CreateSuccessResponse(result));
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Registration failed for user {Username}", registerRequest.Username);

            return Ok(ApiResponse<string>.CreateFailureResponse(AppConstants.GENERIC_ERROR_MESSAGE));
        }
    }
}
