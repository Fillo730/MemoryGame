using MemoryGame_API.IRepositories;
using MemoryGame_API.Models;
using Microsoft.EntityFrameworkCore;

namespace MemoryGame_API.Repositories;

public class LeaderboardRepository (AppDbContext dbContext) : BaseRepository(dbContext), ILeaderboardRepository
{
    public async Task<IEnumerable<TopPlayer>> GetTopPlayersAsync(int topN)
    {
        var gamesCompletedByUserId = _dbContext.GameResults
            .GroupBy(g => g.UserId)
            .Select(gr => new { UserId = gr.Key, GamesCompleted = gr.Count() });

        return await gamesCompletedByUserId
            .Join(_dbContext.Users, c => c.UserId, u => u.Id, (c, u) => new TopPlayer
            {
                Username = u.Username,
                GamesCompleted = c.GamesCompleted
            })
            .OrderByDescending(p => p.GamesCompleted)
            .Take(topN)
            .ToListAsync();
    }

    public async Task<IEnumerable<DifficultyGamesCount>> GetGamesPerDifficultyAsync(string lang)
    {
        return await _dbContext.Difficulties
            .Select(d => new DifficultyGamesCount
            {
                Difficulty = new Difficulty
                {
                    Id = d.Id,
                    NumberOfPairs = d.NumberOfPairs,
                    Translations = d.Translations
                        .Where(t => t.LanguageCode == lang)
                        .ToList()
                },
                GamesPlayed = d.GameResults.Count()
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<DifficultyBestScores>> GetBestScoresPerDifficultyAsync(int topN, string lang)
    {
        var difficulties = await _dbContext.Difficulties
            .Select(d => new Difficulty
            {
                Id = d.Id,
                NumberOfPairs = d.NumberOfPairs,
                Translations = d.Translations
                    .Where(t => t.LanguageCode == lang)
                    .ToList()
            })
            .ToListAsync();

        var gameResults = await _dbContext.GameResults
            .Include(gr => gr.User)
            .ToListAsync();

        return difficulties.Select(d => new DifficultyBestScores
        {
            Difficulty = d,
            TopScores = gameResults
                .Where(gr => gr.DifficultyId == d.Id)
                .OrderBy(gr => gr.Moves)
                .Take(topN)
                .Select(gr => new BestScoreEntry
                {
                    Username = gr.User.Username,
                    Moves = gr.Moves
                })
                .ToList()
        }).ToList();
    }

    public async Task<IEnumerable<DifficultyBestTimes>> GetBestTimesPerDifficultyAsync(int topN, string lang)
    {
        var difficulties = await _dbContext.Difficulties
            .Select(d => new Difficulty
            {
                Id = d.Id,
                NumberOfPairs = d.NumberOfPairs,
                Translations = d.Translations
                    .Where(t => t.LanguageCode == lang)
                    .ToList()
            })
            .ToListAsync();

        var gameResults = await _dbContext.GameResults
            .Include(gr => gr.User)
            .ToListAsync();

        return difficulties.Select(d => new DifficultyBestTimes
        {
            Difficulty = d,
            TopTimes = gameResults
                .Where(gr => gr.DifficultyId == d.Id)
                .OrderBy(gr => gr.DurationSeconds)
                .Take(topN)
                .Select(gr => new BestTimeEntry
                {
                    Username = gr.User.Username,
                    DurationSeconds = gr.DurationSeconds
                })
                .ToList()
        }).ToList();
    }

    public async Task<PlatformStats> GetPlatformStatsAsync(string lang)
    {
        var totalPlayers = await _dbContext.Users.CountAsync();
        var totalGamesPlayed = await _dbContext.GameResults.CountAsync();

        var mostPopular = await _dbContext.Difficulties
            .Select(d => new
            {
                Difficulty = d,
                GamesPlayed = d.GameResults.Count()
            })
            .OrderByDescending(x => x.GamesPlayed)
            .FirstOrDefaultAsync();

        Difficulty? mostPopularDifficulty = null;

        if (mostPopular is not null && mostPopular.GamesPlayed > 0)
        {
            mostPopularDifficulty = new Difficulty
            {
                Id = mostPopular.Difficulty.Id,
                NumberOfPairs = mostPopular.Difficulty.NumberOfPairs,
                Translations = await _dbContext.DifficultyTranslations
                    .Where(t => t.DifficultyId == mostPopular.Difficulty.Id && t.LanguageCode == lang)
                    .ToListAsync()
            };
        }

        return new PlatformStats
        {
            TotalPlayers = totalPlayers,
            TotalGamesPlayed = totalGamesPlayed,
            MostPopularDifficulty = mostPopularDifficulty
        };
    }
}
