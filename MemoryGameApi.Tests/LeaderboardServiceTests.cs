using MemoryGame_API.Dto;
using MemoryGame_API.IMappers;
using MemoryGame_API.IRepositories;
using MemoryGame_API.Models;
using MemoryGame_API.Services;
using Moq;

namespace MemoryGame_API.Tests;

public class LeaderboardServiceTests
{
    private readonly Mock<ILeaderboardRepository> _leaderboardRepository = new();
    private readonly Mock<ILeaderboardMapper> _leaderboardMapper = new();
    private readonly LeaderboardService _sut;

    public LeaderboardServiceTests()
    {
        _sut = new LeaderboardService(_leaderboardRepository.Object, _leaderboardMapper.Object);
    }

    [Fact]
    public async Task GetLeaderboardAsync_ChiedeITopDieciGiocatori()
    {
        _leaderboardRepository
            .Setup(r => r.GetTopPlayersAsync(It.IsAny<int>()))
            .ReturnsAsync(new List<TopPlayer>());
        _leaderboardRepository
            .Setup(r => r.GetGamesPerDifficultyAsync(It.IsAny<string>()))
            .ReturnsAsync(new List<DifficultyGamesCount>());
        _leaderboardRepository
            .Setup(r => r.GetBestScoresPerDifficultyAsync(It.IsAny<int>(), It.IsAny<string>()))
            .ReturnsAsync(new List<DifficultyBestScores>());
        _leaderboardRepository
            .Setup(r => r.GetBestTimesPerDifficultyAsync(It.IsAny<int>(), It.IsAny<string>()))
            .ReturnsAsync(new List<DifficultyBestTimes>());
        _leaderboardMapper
            .Setup(m => m.MapToLeaderboardDto(It.IsAny<Leaderboard>()))
            .Returns(new LeaderboardDto());

        await _sut.GetLeaderboardAsync("it");

        _leaderboardRepository.Verify(r => r.GetTopPlayersAsync(10), Times.Once);
        _leaderboardRepository.Verify(r => r.GetBestScoresPerDifficultyAsync(10, "it"), Times.Once);
        _leaderboardRepository.Verify(r => r.GetBestTimesPerDifficultyAsync(10, "it"), Times.Once);
    }

    [Fact]
    public async Task GetLeaderboardAsync_PassaAlMapperIDatiCombinatiDiTutteLeQuattroFonti()
    {
        var topPlayers = new List<TopPlayer> { new() { Username = "mario", GamesCompleted = 12 } };
        var gamesPerDifficulty = new List<DifficultyGamesCount> { new() { GamesPlayed = 3 } };
        var bestScores = new List<DifficultyBestScores> { new() };
        var bestTimes = new List<DifficultyBestTimes> { new() };

        _leaderboardRepository.Setup(r => r.GetTopPlayersAsync(10)).ReturnsAsync(topPlayers);
        _leaderboardRepository.Setup(r => r.GetGamesPerDifficultyAsync("en")).ReturnsAsync(gamesPerDifficulty);
        _leaderboardRepository.Setup(r => r.GetBestScoresPerDifficultyAsync(10, "en")).ReturnsAsync(bestScores);
        _leaderboardRepository.Setup(r => r.GetBestTimesPerDifficultyAsync(10, "en")).ReturnsAsync(bestTimes);

        Leaderboard? capturedLeaderboard = null;
        _leaderboardMapper
            .Setup(m => m.MapToLeaderboardDto(It.IsAny<Leaderboard>()))
            .Callback<Leaderboard>(l => capturedLeaderboard = l)
            .Returns(new LeaderboardDto());

        await _sut.GetLeaderboardAsync("en");

        Assert.NotNull(capturedLeaderboard);
        Assert.Same(topPlayers, capturedLeaderboard!.TopPlayers);
        Assert.Same(gamesPerDifficulty, capturedLeaderboard.GamesPerDifficulty);
        Assert.Same(bestScores, capturedLeaderboard.BestScoresPerDifficulty);
        Assert.Same(bestTimes, capturedLeaderboard.BestTimesPerDifficulty);
    }

    [Fact]
    public async Task GetLeaderboardAsync_RestituisceEsattamenteQuelloCheProduceIlMapper()
    {
        _leaderboardRepository.Setup(r => r.GetTopPlayersAsync(It.IsAny<int>())).ReturnsAsync(new List<TopPlayer>());
        _leaderboardRepository.Setup(r => r.GetGamesPerDifficultyAsync(It.IsAny<string>())).ReturnsAsync(new List<DifficultyGamesCount>());
        _leaderboardRepository.Setup(r => r.GetBestScoresPerDifficultyAsync(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(new List<DifficultyBestScores>());
        _leaderboardRepository.Setup(r => r.GetBestTimesPerDifficultyAsync(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(new List<DifficultyBestTimes>());

        var expectedDto = new LeaderboardDto
        {
            TopPlayers = new List<TopPlayerDto> { new() { Username = "giulia", GamesCompleted = 7 } }
        };
        _leaderboardMapper.Setup(m => m.MapToLeaderboardDto(It.IsAny<Leaderboard>())).Returns(expectedDto);

        var result = await _sut.GetLeaderboardAsync("it");

        Assert.Same(expectedDto, result);
    }

    [Fact]
    public async Task GetPlatformStatsAsync_DelegaAlRepositoryEMappaIlRisultato()
    {
        var stats = new PlatformStats { TotalPlayers = 100, TotalGamesPlayed = 500 };
        var statsDto = new PlatformStatsDto { TotalPlayers = 100, TotalGamesPlayed = 500 };

        _leaderboardRepository
            .Setup(r => r.GetPlatformStatsAsync("fr"))
            .ReturnsAsync(stats);
        _leaderboardMapper
            .Setup(m => m.MapToPlatformStatsDto(stats))
            .Returns(statsDto);

        var result = await _sut.GetPlatformStatsAsync("fr");

        Assert.Same(statsDto, result);
        _leaderboardRepository.Verify(r => r.GetPlatformStatsAsync("fr"), Times.Once);
    }
}