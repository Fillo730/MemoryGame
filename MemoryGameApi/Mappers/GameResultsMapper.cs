using MemoryGame_API.Dto;
using MemoryGame_API.IMappers;
using MemoryGame_API.Models;

namespace MemoryGame_API.Mappers;

public class GameResultsMapper (IDifficultiesMapper difficultyMapper) : IGameResultsMapper
{
    private readonly IDifficultiesMapper _difficultyMapper = difficultyMapper;
    public GameResult MapDtoToEntity(GameResultDto gameResult, int userId)
    {
        return new GameResult
        {
            Id = gameResult.Id,
            DifficultyId = gameResult.Difficulty.Id,
            UserId = userId,
            Moves = gameResult.Moves,
            PlayedAt = gameResult.PlayedAt,
        };
    }

    public IEnumerable<GameResult> MapDtoToEntityList(IEnumerable<GameResultDto> gameResults, int userId)
    {
        return gameResults.Select(gr => MapDtoToEntity(gr, userId));
    }

    public GameResultDto MapEntityToDto(GameResult gameResult)
    {
        return new GameResultDto
        {
            Id = gameResult.Id,
            Moves = gameResult.Moves,
            Difficulty = _difficultyMapper.MapToDifficultyDto(gameResult.Difficulty),
            PlayedAt = gameResult.PlayedAt,
        };
    }

    public IEnumerable<GameResultDto> MapEntityToDtoList(IEnumerable<GameResult> gameResults)
    {
        return gameResults.Select(gr => MapEntityToDto(gr));
    }
}
