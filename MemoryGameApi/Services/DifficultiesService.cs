using MemoryGame_API.Dto;
using MemoryGame_API.IMappers;
using MemoryGame_API.IRepositories;
using MemoryGame_API.IServices;

namespace MemoryGame_API.Services;

public class DifficultiesService (IDifficultiesRepository difficultiesRepository, IDifficultiesMapper difficultiesMapper) : IDifficultiesService
{
    private readonly IDifficultiesRepository _difficultiesRepository = difficultiesRepository;

    private readonly IDifficultiesMapper _difficultiesMapper = difficultiesMapper;
    public async Task<IEnumerable<DifficultyDto>> GetDifficultiesAsync(string lang)
    {
        var result = await _difficultiesRepository.GetAllDifficultiesAsync(lang);

        return _difficultiesMapper.MapToDifficultyDtoList(result);
    }
}
