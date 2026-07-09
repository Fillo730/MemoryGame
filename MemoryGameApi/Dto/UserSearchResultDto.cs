namespace MemoryGame_API.Dto;

public class UserSearchResultDto
{
    public int UserId { get; set; }

    public string Username { get; set; } = string.Empty;

    public string Status { get; set; } = "None";
}
