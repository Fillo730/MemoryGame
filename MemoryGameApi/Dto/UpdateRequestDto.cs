namespace MemoryGame_API.Dto;

public class UpdateRequestDto
{
    public string Username { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string? Bio { get; set; }

    public string? Country { get; set; }

    public DateTime? BirthDate { get; set; }
}
