using MemoryGame_API.Dto;
using MemoryGame_API.Models;

namespace MemoryGame_API.IMappers;

public interface IStatisticalMapper
{
    IEnumerable<UserStatsDto> MapUserStatToDtoList(IEnumerable<UserStats> userStats);

    UserStatsDto MapToUserStatDto(UserStats userStats);
}
