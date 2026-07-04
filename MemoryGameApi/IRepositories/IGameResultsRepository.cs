using MemoryGame_API.Models;

namespace MemoryGame_API.IRepositories;

public interface IGameResultsRepository
{
    Task<GameResult> AddGameResultAsync(GameResult gameResult);

    Task<IEnumerable<GameResult>> GetGameResultForUserByIdAsync(int id);

    Task<IEnumerable<UserStats>> GetUserStatsByIdAsync(int id, string lang);

    Task SaveChangesAsync();
}
