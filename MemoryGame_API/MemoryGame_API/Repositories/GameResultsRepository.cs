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
            .ToListAsync();
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
            })
            .ToListAsync();

        return stats;
    }
}
