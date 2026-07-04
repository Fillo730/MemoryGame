using MemoryGame_API.Dto;
using MemoryGame_API.Models;

namespace MemoryGame_API.IMappers;

public interface IGameResultsMapper
{
    GameResult MapDtoToEntity(GameResultDto gameResult, int userId);

    IEnumerable<GameResult> MapDtoToEntityList(IEnumerable<GameResultDto> gameResults, int userId);

    GameResultDto MapEntityToDto(GameResult gameResult);

    IEnumerable<GameResultDto> MapEntityToDtoList(IEnumerable<GameResult> gameResults);
}
