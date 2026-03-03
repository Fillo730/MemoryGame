namespace MemoryGame_API.Dto;

public class GameResultDto
{
    public int Id { get; set; }
    public int Moves { get; set; }

    public DateTime PlayedAt { get; set; }

    public DifficultyDto Difficulty { get; set; }
}
