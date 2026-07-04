using MemoryGame_API.Dto;
using MemoryGame_API.Models;

namespace MemoryGame_API.IMappers;

public interface IDifficultiesMapper
{
    IEnumerable<DifficultyDto> MapToDifficultyDtoList(IEnumerable<Difficulty> difficulties);

    DifficultyDto MapToDifficultyDto(Difficulty difficulty);
}
