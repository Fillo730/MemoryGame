using MemoryGame_API.Dto;
using MemoryGame_API.IMappers;
using MemoryGame_API.IRepositories;
using MemoryGame_API.IServices;

namespace MemoryGame_API.Services;

public class GameResultsService (IGameResultsRepository gameResultsRepository, IGameResultsMapper gameResultsMapper, IStatisticalMapper statisticalMapper, IAchievementsService achievementsService): IGameResultsService
{
    private readonly IGameResultsRepository _gameResultsRepository = gameResultsRepository;

    private readonly IGameResultsMapper _gameResultsMapper = gameResultsMapper;

    private readonly IStatisticalMapper _statisticalMapper = statisticalMapper;

    private readonly IAchievementsService _achievementsService = achievementsService;
    public async Task<GameResultDto> AddGameResutlAsync(GameResultDto gameResultDto, int userId)
    {
        var gameResultEntity = _gameResultsMapper.MapDtoToEntity(gameResultDto, userId);

        await _gameResultsRepository.AddGameResultAsync(gameResultEntity);

        await _gameResultsRepository.SaveChangesAsync();

        await _achievementsService.EvaluateAndUnlockNewAsync(userId);

        await _gameResultsRepository.SaveChangesAsync();

        gameResultDto.Id = gameResultEntity.Id;

        return gameResultDto;
    }

    public Task<IEnumerable<GameResultDto>> GetGameResultsForUserByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<UserStatsDto>> GetUserStatsByIdAsync(int id, string lang)
    {
        var result = await _gameResultsRepository.GetUserStatsByIdAsync(id, lang);

        return _statisticalMapper.MapUserStatToDtoList(result);
    }
}
