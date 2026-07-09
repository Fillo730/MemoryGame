using MemoryGame_API.IRepositories;
using MemoryGame_API.Models;
using Microsoft.EntityFrameworkCore;

namespace MemoryGame_API.Repositories;

public class GameResultsRepository (AppDbContext dbContext) : BaseRepository(dbContext), IGameResultsRepository
{
    public async Task<GameResult> AddGameResultAsync(GameResult gameResult)
    {
        await _dbContext.GameResults.AddAsync(gameResult);

        return gameResult;
    }

    public async Task<IEnumerable<GameResult>> GetGameResultForUserByIdAsync(int id)
    {
        return await _dbContext.GameResults
            .Where(g => g.UserId == id)
            .Include(g => g.Difficulty)
            .ToListAsync();
    }

    public async Task<(IEnumerable<GameResult> Items, int TotalCount)> GetGameHistoryForUserByIdAsync(int id, string lang, int page, int pageSize)
    {
        var query = _dbContext.GameResults
            .Where(g => g.UserId == id)
            .OrderByDescending(g => g.PlayedAt);

        var totalCount = await query.CountAsync();

        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(g => new GameResult
            {
                Id = g.Id,
                Moves = g.Moves,
                DurationSeconds = g.DurationSeconds,
                PlayedAt = g.PlayedAt,
                DifficultyId = g.DifficultyId,
                Difficulty = new Difficulty
                {
                    Id = g.Difficulty.Id,
                    NumberOfPairs = g.Difficulty.NumberOfPairs,
                    Translations = g.Difficulty.Translations
                        .Where(t => t.LanguageCode == lang)
                        .ToList()
                }
            })
            .ToListAsync();

        return (items, totalCount);
    }

    public async Task<IEnumerable<UserStats>> GetUserStatsByIdAsync(int id, string lang)
    {
        var stats = await _dbContext.Difficulties
            .Select(d => new UserStats
            {
                Difficulty = new Difficulty
                {
                    Id = d.Id,
                    NumberOfPairs = d.NumberOfPairs,
                    Translations = d.Translations
                        .Where(t => t.LanguageCode == lang)
                        .ToList()
                },
                GamesPlayed = d.GameResults.Count(gr => gr.UserId == id),
                TotalMoves = d.GameResults.Where(gr => gr.UserId == id).Sum(gr => (int?)gr.Moves) ?? 0,
                BestScore = d.GameResults.Where(gr => gr.UserId == id).Min(gr => (int?)gr.Moves) ?? 0,
                TotalDurationSeconds = d.GameResults.Where(gr => gr.UserId == id).Sum(gr => (int?)gr.DurationSeconds) ?? 0,
            })
            .ToListAsync();

        return stats;
    }
}
