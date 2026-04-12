using MemoryGame_API.Dto;
using MemoryGame_API.IMappers;
using MemoryGame_API.Models;

namespace MemoryGame_API.Mappers;

public class DifficultiesMapper : IDifficultiesMapper
{
    public DifficultyDto MapToDifficultyDto(Difficulty difficulty)
    {
        return new DifficultyDto
        {
            Id = difficulty.Id,
            Label = difficulty.Translations.FirstOrDefault()?.Label ?? "N/A",
            NumberOfPairs = difficulty.NumberOfPairs,
        };
    }

    public IEnumerable<DifficultyDto> MapToDifficultyDtoList(IEnumerable<Difficulty> difficulties)
    {
        return difficulties.Select(d => MapToDifficultyDto(d));
    }
}
