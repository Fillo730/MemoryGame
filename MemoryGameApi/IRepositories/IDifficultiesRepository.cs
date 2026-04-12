using MemoryGame_API.Models;

namespace MemoryGame_API.IRepositories;

public interface IDifficultiesRepository
{
    Task<IEnumerable<Difficulty>> GetAllDifficultiesAsync(string lang);
}