namespace MemoryGame_API.Dto;

public class DifficultyDto
{
    public int Id { get; set; }
    public string Label { get; set; } = string.Empty;

    public int NumberOfPairs { get; set; }
}
