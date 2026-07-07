using MemoryGame_API.Dto;
using MemoryGame_API.IMappers;
using MemoryGame_API.IRepositories;
using MemoryGame_API.IServices;

namespace MemoryGame_API.Services;

public class AchievementsService (
    IAchievementsRepository achievementsRepository,
    IGameResultsRepository gameResultsRepository,
    IAchievementRulesEvaluator rulesEvaluator,
    IAchievementsMapper achievementsMapper) : IAchievementsService
{
    private readonly IAchievementsRepository _achievementsRepository = achievementsRepository;

    private readonly IGameResultsRepository _gameResultsRepository = gameResultsRepository;

    private readonly IAchievementRulesEvaluator _rulesEvaluator = rulesEvaluator;

    private readonly IAchievementsMapper _achievementsMapper = achievementsMapper;

    public async Task<IEnumerable<AchievementDto>> GetAchievementsForUserAsync(int userId, string lang)
    {
        var catalog = await _achievementsRepository.GetAllWithTranslationsAsync(lang);

        var unlockedByAchievementId = (await _achievementsRepository.GetUnlockedForUserAsync(userId))
            .ToDictionary(ua => ua.AchievementId, ua => ua.UnlockedAt);

        return _achievementsMapper.MapToAchievementDtoList(catalog, unlockedByAchievementId);
    }

    public async Task EvaluateAndUnlockNewAsync(int userId)
    {
        var gameResults = await _gameResultsRepository.GetGameResultForUserByIdAsync(userId);
        var difficulties = (await _achievementsRepository.GetDifficultyBenchmarksAsync()).ToList();

        if (difficulties.Count == 0)
        {
            return;
        }

        var hardestDifficultyId = difficulties.OrderByDescending(d => d.NumberOfPairs).First().Id;

        var satisfiedCodes = _rulesEvaluator.EvaluateUnlockedCodes(gameResults, difficulties.Count, hardestDifficultyId);

        var alreadyUnlockedIds = (await _achievementsRepository.GetUnlockedForUserAsync(userId))
            .Select(ua => ua.AchievementId)
            .ToHashSet();

        var newAchievementIds = (await _achievementsRepository.GetAllAsync())
            .Where(a => satisfiedCodes.Contains(a.Code) && !alreadyUnlockedIds.Contains(a.Id))
            .Select(a => a.Id)
            .ToList();

        if (newAchievementIds.Count > 0)
        {
            await _achievementsRepository.AddUnlockedAsync(userId, newAchievementIds);
        }
    }
}
