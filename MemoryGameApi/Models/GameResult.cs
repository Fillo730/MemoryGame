namespace MemoryGame_API.Models;

public class GameResult
{
    public int Id { get; set; }

    public int Moves { get; set; }

    public DateTime PlayedAt { get; set; } = DateTime.UtcNow;

    public int UserId { get; set; }

    public virtual User User { get; set; }

    public int DifficultyId { get; set; }

    public virtual Difficulty Difficulty { get; set; }

}
