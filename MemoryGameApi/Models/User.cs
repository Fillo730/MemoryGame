using MemoryGame_API.Utils;

namespace MemoryGame_API.Models;

public class User
{
    public int Id { get; set; }

    public string Username { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public UserRoleEnum Role {  get; set; }

    public virtual ICollection<GameResult> GameResults { get; set; } = new List<GameResult>();
}
