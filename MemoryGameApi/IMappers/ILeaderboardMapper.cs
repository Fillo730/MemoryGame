using MemoryGame_API.Dto;
using MemoryGame_API.Models;

namespace MemoryGame_API.IMappers;

public interface ILeaderboardMapper
{
    LeaderboardDto MapToLeaderboardDto(Leaderboard leaderboard);
}
