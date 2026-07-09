using MemoryGame_API.Models;

namespace MemoryGame_API.IRepositories;

public interface IGameResultsRepository
{
    Task<GameResult> AddGameResultAsync(GameResult gameResult);

    Task<IEnumerable<GameResult>> GetGameResultForUserByIdAsync(int id);

    Task<(IEnumerable<GameResult> Items, int TotalCount)> GetGameHistoryForUserByIdAsync(int id, string lang, int page, int pageSize);

    Task<IEnumerable<UserStats>> GetUserStatsByIdAsync(int id, string lang);

    Task SaveChangesAsync();
}
