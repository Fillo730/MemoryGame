using MemoryGame_API.Dto;

namespace MemoryGame_API.IServices;

public interface ILeaderboardService
{
    Task<LeaderboardDto> GetLeaderboardAsync(string lang);
}
