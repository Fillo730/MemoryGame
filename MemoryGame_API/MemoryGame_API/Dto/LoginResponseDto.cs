using MemoryGame_API.Utils;

namespace MemoryGame_API.Dto;

public class LoginResponseDto
{
    public string Token { get; set; } = string.Empty;

    public string Username { get; set; } = string.Empty;

    public string Email {  get; set; } = string.Empty;

    public UserRoleEnum UserRole { get; set; }

    public int Id { get; set; }
}
