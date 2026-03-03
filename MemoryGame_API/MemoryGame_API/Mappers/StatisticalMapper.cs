using MemoryGame_API.Dto;
using MemoryGame_API.IMappers;
using MemoryGame_API.Models;

namespace MemoryGame_API.Mappers;

public class StatisticalMapper (IDifficultiesMapper difficultiesMapper) : IStatisticalMapper
{
    private readonly IDifficultiesMapper _difficultiesMapper = difficultiesMapper;
    public UserStatsDto MapToUserStatDto(UserStats userStats)
    {
        return new UserStatsDto
        {
            Difficulty = _difficultiesMapper.MapToDifficultyDto(userStats.Difficulty),
            GamesPlayed = userStats.GamesPlayed,
            TotalMoves = userStats.TotalMoves,
            BestScore = userStats.BestScore,
            AverageMovesPerGame = userStats.BestScore == 0 ? 0 : ((double)userStats.TotalMoves / userStats.BestScore)
        };
    }

    public IEnumerable<UserStatsDto> MapUserStatToDtoList(IEnumerable<UserStats> userStats)
    {
        return userStats.Select(us => MapToUserStatDto(us));
    }
}
