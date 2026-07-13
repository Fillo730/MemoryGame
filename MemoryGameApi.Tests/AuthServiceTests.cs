using MemoryGame_API.Dto;
using MemoryGame_API.IMappers;
using MemoryGame_API.IRepositories;
using MemoryGame_API.IServices;
using MemoryGame_API.Models;
using MemoryGame_API.Services;
using Moq;

namespace MemoryGame_API.Tests;

public class AuthServiceTests
{
    private readonly Mock<IUserRepository> _userRepository = new();
    private readonly Mock<ITokenService> _tokenService = new();
    private readonly Mock<IAuthMapper> _authMapper = new();
    private readonly AuthService _sut;

    public AuthServiceTests()
    {
        _sut = new AuthService(_userRepository.Object, _tokenService.Object, _authMapper.Object);
    }

    private static User MakeUser(string username = "mario", string password = "Password123!")
    {
        return new User
        {
            Id = 1,
            Username = username,
            Email = $"{username}@example.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password)
        };
    }

    [Fact]
    public async Task LoginUserAsync_UtenteNonEsiste_RestituisceNull()
    {
        _userRepository
            .Setup(r => r.GetUserByUsernameAsync(It.IsAny<string>()))
            .ReturnsAsync((User?)null);

        var request = new LoginRequestDto { Username = "nonEsiste", Password = "qualsiasi" };

        var result = await _sut.LoginUserAsync(request);

        Assert.Null(result);
        _tokenService.Verify(t => t.GenerateToken(It.IsAny<User>()), Times.Never);
    }

    [Fact]
    public async Task LoginUserAsync_PasswordErrata_RestituisceNull()
    {
        var user = MakeUser(password: "PasswordGiusta1!");
        _userRepository
            .Setup(r => r.GetUserByUsernameAsync(user.Username))
            .ReturnsAsync(user);

        var request = new LoginRequestDto { Username = user.Username, Password = "PasswordSbagliata" };

        var result = await _sut.LoginUserAsync(request);

        Assert.Null(result);
        _tokenService.Verify(t => t.GenerateToken(It.IsAny<User>()), Times.Never);
    }

    [Fact]
    public async Task LoginUserAsync_CredenzialiCorrette_RestituisceTokenELoginResponse()
    {
        var user = MakeUser(password: "PasswordGiusta1!");
        var expectedExpiry = DateTime.UtcNow.AddHours(1);

        _userRepository
            .Setup(r => r.GetUserByUsernameAsync(user.Username))
            .ReturnsAsync(user);

        _tokenService
            .Setup(t => t.GenerateToken(user))
            .Returns(("fake-jwt-token", expectedExpiry));

        _authMapper
            .Setup(m => m.MapUserToLoginResponseDto(user, "fake-jwt-token", expectedExpiry))
            .Returns(new LoginResponseDto { Token = "fake-jwt-token", Username = user.Username });

        var request = new LoginRequestDto { Username = user.Username, Password = "PasswordGiusta1!" };

        var result = await _sut.LoginUserAsync(request);

        Assert.NotNull(result);
        Assert.Equal("fake-jwt-token", result!.Token);
        Assert.Equal(user.Username, result.Username);
    }

    [Fact]
    public async Task RegisterUserAsync_UsernameGiaEsistente_RestituisceNull()
    {
        _userRepository
            .Setup(r => r.GetUserByUsernameAsync("marioRossi"))
            .ReturnsAsync(MakeUser("marioRossi"));
        _userRepository
            .Setup(r => r.GetUserByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync((User?)null);

        var request = new RegisterRequestDto
        {
            Username = "marioRossi",
            Email = "nuovo@example.com",
            Password = "Password123!"
        };

        var result = await _sut.RegisterUserAsync(request);

        Assert.Null(result);
        _userRepository.Verify(r => r.AddUserAsync(It.IsAny<User>()), Times.Never);
    }

    [Fact]
    public async Task RegisterUserAsync_EmailGiaEsistente_RestituisceNull()
    {
        _userRepository
            .Setup(r => r.GetUserByUsernameAsync(It.IsAny<string>()))
            .ReturnsAsync((User?)null);
        _userRepository
            .Setup(r => r.GetUserByEmailAsync("mario@example.com"))
            .ReturnsAsync(MakeUser("qualcunAltro"));

        var request = new RegisterRequestDto
        {
            Username = "nuovoUsername",
            Email = "mario@example.com",
            Password = "Password123!"
        };

        var result = await _sut.RegisterUserAsync(request);

        Assert.Null(result);
        _userRepository.Verify(r => r.AddUserAsync(It.IsAny<User>()), Times.Never);
    }

    [Fact]
    public async Task RegisterUserAsync_DatiValidi_CreaUtenteConPasswordHashata()
    {
        _userRepository
            .Setup(r => r.GetUserByUsernameAsync(It.IsAny<string>()))
            .ReturnsAsync((User?)null);
        _userRepository
            .Setup(r => r.GetUserByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync((User?)null);

        var newUser = new User { Username = "nuovo", Email = "nuovo@example.com" };
        _authMapper
            .Setup(m => m.MapRegisterRequestDtoToUser(It.IsAny<RegisterRequestDto>()))
            .Returns(newUser);

        _tokenService
            .Setup(t => t.GenerateToken(It.IsAny<User>()))
            .Returns(("fake-jwt-token", DateTime.UtcNow.AddHours(1)));

        _authMapper
            .Setup(m => m.MapUserToLoginResponseDto(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<DateTime>()))
            .Returns(new LoginResponseDto { Token = "fake-jwt-token", Username = "nuovo" });

        var request = new RegisterRequestDto
        {
            Username = "nuovo",
            Email = "nuovo@example.com",
            Password = "Password123!"
        };

        var result = await _sut.RegisterUserAsync(request);

        Assert.NotNull(result);
        Assert.NotEqual("Password123!", newUser.PasswordHash);
        Assert.True(BCrypt.Net.BCrypt.Verify("Password123!", newUser.PasswordHash));

        _userRepository.Verify(r => r.AddUserAsync(newUser), Times.Once);
        _userRepository.Verify(r => r.SaveChangesAsync(), Times.Once);
    }
}