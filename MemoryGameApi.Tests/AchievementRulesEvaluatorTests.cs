using MemoryGame_API.Models;
using MemoryGame_API.Services;
using MemoryGame_API.Utils;
namespace MemoryGame_API.Tests;
public class AchievementRulesEvaluatorTests
{
    private static GameResult MakeGame(
        int difficultyId = 1,
        int numberOfPairs = 6,
        int? moves = null,
        DateTime? playedAt = null)
    {
        var difficulty = new Difficulty { Id = difficultyId, NumberOfPairs = numberOfPairs };
        return new GameResult
        {
            DifficultyId = difficultyId,
            Difficulty = difficulty,
            Moves = moves ?? numberOfPairs * 2,
            PlayedAt = playedAt ?? new DateTime(2025, 1, 8, 14, 0, 0, DateTimeKind.Utc)
        };
    }
    private readonly AchievementRulesEvaluator _sut = new();
    [Fact]
    public void EvaluateUnlockedCodes_ConUnaPartita_SbloccaFirstWin()
    {
        var games = new List<GameResult> { MakeGame() };
        var unlocked = _sut.EvaluateUnlockedCodes(games, totalDifficultyCount: 9, hardestDifficultyId: 9);
        Assert.Contains(AchievementCodes.FirstWin, unlocked);
    }
    [Fact]
    public void EvaluateUnlockedCodes_SenzaPartite_NonSbloccaNulla()
    {
        var games = new List<GameResult>();
        var unlocked = _sut.EvaluateUnlockedCodes(games, totalDifficultyCount: 9, hardestDifficultyId: 9);
        Assert.Empty(unlocked);
    }
    [Theory]
    [InlineData(1, false)]    // 1 partita -> TenGames NON ancora
    [InlineData(9, false)]    // 9 partite -> ancora no
    [InlineData(10, true)]    // 10 partite -> TenGames sblocca
    [InlineData(25, true)]    // oltre soglia -> resta sbloccato
    public void EvaluateUnlockedCodes_TenGames_ScattaAllaDecimaPartita(int count, bool shouldUnlock)
    {
        var games = Enumerable.Range(0, count).Select(_ => MakeGame()).ToList();
        var unlocked = _sut.EvaluateUnlockedCodes(games, totalDifficultyCount: 9, hardestDifficultyId: 9);
        Assert.Equal(shouldUnlock, unlocked.Contains(AchievementCodes.TenGames));
    }
    [Fact]
    public void EvaluateUnlockedCodes_PartitaAlleTre_SbloccaNightOwl()
    {
        var games = new List<GameResult>
        {
            MakeGame(playedAt: new DateTime(2025, 1, 8, 3, 0, 0, DateTimeKind.Utc))
        };
        var unlocked = _sut.EvaluateUnlockedCodes(games, totalDifficultyCount: 9, hardestDifficultyId: 9);
        Assert.Contains(AchievementCodes.NightOwl, unlocked);
    }
    [Fact]
    public void EvaluateUnlockedCodes_PartitaDiPomeriggio_NonSbloccaNightOwl()
    {
        var games = new List<GameResult>
        {
            MakeGame(playedAt: new DateTime(2025, 1, 8, 14, 0, 0, DateTimeKind.Utc))
        };
        var unlocked = _sut.EvaluateUnlockedCodes(games, totalDifficultyCount: 9, hardestDifficultyId: 9);
        Assert.DoesNotContain(AchievementCodes.NightOwl, unlocked);
    }
    [Fact]
    public void EvaluateUnlockedCodes_PartitaDiSabato_SbloccaWeekendWarrior()
    {
        var games = new List<GameResult>
        {
            MakeGame(playedAt: new DateTime(2025, 1, 11, 14, 0, 0, DateTimeKind.Utc))
        };
        var unlocked = _sut.EvaluateUnlockedCodes(games, totalDifficultyCount: 9, hardestDifficultyId: 9);
        Assert.Contains(AchievementCodes.WeekendWarrior, unlocked);
    }
    [Fact]
    public void EvaluateUnlockedCodes_GiocateTutteLeDifficolta_SbloccaAllDifficulties()
    {
        var games = Enumerable.Range(1, 9)
            .Select(id => MakeGame(difficultyId: id))
            .ToList();
        var unlocked = _sut.EvaluateUnlockedCodes(games, totalDifficultyCount: 9, hardestDifficultyId: 9);
        Assert.Contains(AchievementCodes.AllDifficulties, unlocked);
    }
    [Fact]
    public void EvaluateUnlockedCodes_MancaUnaDifficolta_NonSbloccaAllDifficulties()
    {
        var games = Enumerable.Range(1, 8)
            .Select(id => MakeGame(difficultyId: id))
            .ToList();
        var unlocked = _sut.EvaluateUnlockedCodes(games, totalDifficultyCount: 9, hardestDifficultyId: 9);
        Assert.DoesNotContain(AchievementCodes.AllDifficulties, unlocked);
    }
    [Fact]
    public void EvaluateUnlockedCodes_GiocataDifficoltaMassima_SbloccaHardestDifficulty()
    {
        var games = new List<GameResult> { MakeGame(difficultyId: 9) };
        var unlocked = _sut.EvaluateUnlockedCodes(games, totalDifficultyCount: 9, hardestDifficultyId: 9);
        Assert.Contains(AchievementCodes.HardestDifficulty, unlocked);
    }
}