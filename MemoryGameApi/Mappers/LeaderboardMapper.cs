using MemoryGame_API.Dto;
using MemoryGame_API.IMappers;
using MemoryGame_API.Models;

namespace MemoryGame_API.Mappers;

public class LeaderboardMapper (IDifficultiesMapper difficultiesMapper) : ILeaderboardMapper
{
    private readonly IDifficultiesMapper _difficultiesMapper = difficultiesMapper;

    public LeaderboardDto MapToLeaderboardDto(Leaderboard leaderboard)
    {
        return new LeaderboardDto
        {
            TopPlayers = leaderboard.TopPlayers.Select(MapTopPlayer),
            GamesPerDifficulty = leaderboard.GamesPerDifficulty.Select(MapDifficultyGamesCount),
            BestScoresPerDifficulty = leaderboard.BestScoresPerDifficulty.Select(MapDifficultyBestScores)
        };
    }

    private static TopPlayerDto MapTopPlayer(TopPlayer topPlayer)
    {
        return new TopPlayerDto
        {
            Username = topPlayer.Username,
            GamesCompleted = topPlayer.GamesCompleted
        };
    }

    private DifficultyGamesCountDto MapDifficultyGamesCount(DifficultyGamesCount gamesCount)
    {
        return new DifficultyGamesCountDto
        {
            Difficulty = _difficultiesMapper.MapToDifficultyDto(gamesCount.Difficulty),
            GamesPlayed = gamesCount.GamesPlayed
        };
    }

    private DifficultyBestScoresDto MapDifficultyBestScores(DifficultyBestScores bestScores)
    {
        return new DifficultyBestScoresDto
        {
            Difficulty = _difficultiesMapper.MapToDifficultyDto(bestScores.Difficulty),
            TopScores = bestScores.TopScores.Select(s => new BestScoreEntryDto
            {
                Username = s.Username,
                Moves = s.Moves
            })
        };
    }
}
