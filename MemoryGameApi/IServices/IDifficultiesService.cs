using MemoryGame_API.Dto;

namespace MemoryGame_API.IServices;

public interface IDifficultiesService
{
    Task<IEnumerable<DifficultyDto>> GetDifficultiesAsync(string lang); 
}
