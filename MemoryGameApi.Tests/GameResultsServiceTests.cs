using MemoryGame_API.Dto;
using MemoryGame_API.IMappers;
using MemoryGame_API.IRepositories;
using MemoryGame_API.IServices;
using MemoryGame_API.Models;
using MemoryGame_API.Services;
using Moq;

namespace MemoryGame_API.Tests;

public class GameResultsServiceTests
{
    private readonly Mock<IGameResultsRepository> _gameResultsRepository = new();
    private readonly Mock<IGameResultsMapper> _gameResultsMapper = new();
    private readonly Mock<IStatisticalMapper> _statisticalMapper = new();
    private readonly Mock<IAchievementsService> _achievementsService = new();
    private readonly GameResultsService _sut;

    public GameResultsServiceTests()
    {
        _sut = new GameResultsService(
            _gameResultsRepository.Object,
            _gameResultsMapper.Object,
            _statisticalMapper.Object,
            _achievementsService.Object);
    }

    private static GameResultDto MakeDto(int moves = 10, int durationSeconds = 60)
    {
        return new GameResultDto
        {
            Moves = moves,
            DurationSeconds = durationSeconds,
            PlayedAt = DateTime.UtcNow,
            Difficulty = new DifficultyDto { Id = 1, Label = "Easy", NumberOfPairs = 6 }
        };
    }

    [Fact]
    public async Task AddGameResutlAsync_SalvaIlRisultatoEValutaGliAchievement()
    {
        var dto = MakeDto();
        var entity = new GameResult { Id = 42, Moves = dto.Moves, DurationSeconds = dto.DurationSeconds, UserId = 7 };

        _gameResultsMapper
            .Setup(m => m.MapDtoToEntity(dto, 7))
            .Returns(entity);

        var result = await _sut.AddGameResutlAsync(dto, 7);

        Assert.Equal(42, result.Id);
        _gameResultsRepository.Verify(r => r.AddGameResultAsync(entity), Times.Once);
        _achievementsService.Verify(a => a.EvaluateAndUnlockNewAsync(7), Times.Once);
        _gameResultsRepository.Verify(r => r.SaveChangesAsync(), Times.Exactly(2));
    }

    [Fact]
    public async Task AddGameResutlAsync_SalvaPrimaDiValutareGliAchievement()
    {
        var dto = MakeDto();
        var entity = new GameResult { Id = 1, UserId = 3 };
        var callOrder = new List<string>();

        _gameResultsMapper
            .Setup(m => m.MapDtoToEntity(dto, 3))
            .Returns(entity);

        _gameResultsRepository
            .Setup(r => r.AddGameResultAsync(entity))
            .Callback(() => callOrder.Add("AddGameResult"))
            .ReturnsAsync(entity);

        _achievementsService
            .Setup(a => a.EvaluateAndUnlockNewAsync(3))
            .Callback(() => callOrder.Add("EvaluateAchievements"))
            .Returns(Task.CompletedTask);

        await _sut.AddGameResutlAsync(dto, 3);

        Assert.Equal(new List<string> { "AddGameResult", "EvaluateAchievements" }, callOrder);
    }

    [Fact]
    public async Task GetGameHistoryForUserByIdAsync_RestituisceItemsETotalCountDelRepository()
    {
        var entities = new List<GameResult> { new() { Id = 1 }, new() { Id = 2 } };
        var dtos = new List<GameResultDto> { new() { Id = 1 }, new() { Id = 2 } };

        _gameResultsRepository
            .Setup(r => r.GetGameHistoryForUserByIdAsync(9, "it", 1, 10))
            .ReturnsAsync((entities, 25));

        _gameResultsMapper
            .Setup(m => m.MapEntityToDtoList(entities))
            .Returns(dtos);

        var result = await _sut.GetGameHistoryForUserByIdAsync(9, "it", 1, 10);

        Assert.Equal(2, result.Items.Count());
        Assert.Equal(25, result.TotalCount);
        Assert.Equal(1, result.Page);
        Assert.Equal(10, result.PageSize);
    }

    [Fact]
    public async Task GetGameHistoryForUserByIdAsync_NessunaPartita_RestituisceListaVuotaETotalCountZero()
    {
        _gameResultsRepository
            .Setup(r => r.GetGameHistoryForUserByIdAsync(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync((new List<GameResult>(), 0));

        _gameResultsMapper
            .Setup(m => m.MapEntityToDtoList(It.IsAny<IEnumerable<GameResult>>()))
            .Returns(new List<GameResultDto>());

        var result = await _sut.GetGameHistoryForUserByIdAsync(1, "en", 1, 10);

        Assert.Empty(result.Items);
        Assert.Equal(0, result.TotalCount);
    }

    [Fact]
    public async Task GetUserStatsByIdAsync_RestituisceStatisticheMappate()
    {
        var stats = new List<UserStats>
        {
            new() { Difficulty = new Difficulty { Id = 1 }, GamesPlayed = 5, BestScore = 12 }
        };
        var statsDto = new List<UserStatsDto> { new() { GamesPlayed = 5, BestScore = 12 } };

        _gameResultsRepository
            .Setup(r => r.GetUserStatsByIdAsync(4, "it"))
            .ReturnsAsync(stats);

        _statisticalMapper
            .Setup(m => m.MapUserStatToDtoList(stats))
            .Returns(statsDto);

        var result = await _sut.GetUserStatsByIdAsync(4, "it");

        Assert.Single(result);
        Assert.Equal(5, result.First().GamesPlayed);
    }
}