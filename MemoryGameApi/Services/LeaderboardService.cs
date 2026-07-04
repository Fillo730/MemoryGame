using MemoryGame_API.Dto;
using MemoryGame_API.IMappers;
using MemoryGame_API.IRepositories;
using MemoryGame_API.IServices;
using MemoryGame_API.Models;

namespace MemoryGame_API.Services;

public class LeaderboardService (ILeaderboardRepository leaderboardRepository, ILeaderboardMapper leaderboardMapper) : ILeaderboardService
{
    private const int TopN = 10;

    private readonly ILeaderboardRepository _leaderboardRepository = leaderboardRepository;

    private readonly ILeaderboardMapper _leaderboardMapper = leaderboardMapper;

    public async Task<LeaderboardDto> GetLeaderboardAsync(string lang)
    {
        var leaderboard = new Leaderboard
        {
            TopPlayers = await _leaderboardRepository.GetTopPlayersAsync(TopN),
            GamesPerDifficulty = await _leaderboardRepository.GetGamesPerDifficultyAsync(lang),
            BestScoresPerDifficulty = await _leaderboardRepository.GetBestScoresPerDifficultyAsync(TopN, lang)
        };

        return _leaderboardMapper.MapToLeaderboardDto(leaderboard);
    }
}
