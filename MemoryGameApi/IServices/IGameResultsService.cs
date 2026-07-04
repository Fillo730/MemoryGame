using MemoryGame_API.Dto;
using MemoryGame_API.Models;

namespace MemoryGame_API.IServices;

public interface IGameResultsService
{
    Task<GameResultDto> AddGameResutlAsync(GameResultDto gameResultDto, int id);

    Task<IEnumerable<UserStatsDto>> GetUserStatsByIdAsync(int id, string lang);

    Task<IEnumerable<GameResultDto>> GetGameResultsForUserByIdAsync(int id);
}
