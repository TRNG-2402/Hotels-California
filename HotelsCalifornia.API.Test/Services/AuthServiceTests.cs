namespace HotelsCalifornia.Test.Services;
using HotelsCalifornia.Services;
using HotelsCalifornia.Data;
using HotelsCalifornia.Models;
using HotelsCalifornia.DTOs;
using Moq;
using Microsoft.Extensions.Configuration;

public class AuthServiceTests
{
    private readonly Mock<IUserRepository> _mockRepo;
    private readonly Mock<IConfiguration> _mockConfig;
    private readonly AuthService _sut;

    private const string MOCK_KEY = "IAmNotRealIAmNotRealIAmNotRealIAmNotRealIAmNotRealIAmNotReal";
    private const string MOCK_ISSUER = "MockIssuer";
    private const string MOCK_AUDIENCE = "MockAudience";

    public AuthServiceTests()
    {
        _mockRepo = new();
        _mockConfig = new();
        _sut = new(_mockRepo.Object, _mockConfig.Object);
    }

    // Login

    [Theory]
    [InlineData("", "password")]
    [InlineData("username", "")]
    public async Task Login_InvalidParams_ThrowsUnauthorizedAccessException(string username,
        string password)
    {
        LoginRequestDTO input = new()
        {
            Username = username,
            PasswordHash = password
        };
        await Assert.ThrowsAsync<UnauthorizedAccessException>(
            () => _sut.Login(input)
        );
    }

    [Theory]
    [InlineData("username1", "password")]
    [InlineData("username", "password1")]
    public async Task Login_WrongUsernameOrPassword_ThrowsUnauthorizedAccessException(string username,
        string password)
    {
        LoginRequestDTO input = new()
        {
            Username = username,
            PasswordHash = password
        };
        BuildTokenDTO repoResponse = new()
        {
            NameIdentifier = "1",
            Name = "username",
            PasswordHash = "password",
            Role = "Example"
        };
        _mockRepo.Setup(x => x.GetUserByUsernameAsync("username"))
            .ReturnsAsync(repoResponse);
        _mockRepo.Setup(x => x.GetUserByUsernameAsync("username1"))
            .ThrowsAsync(new UnauthorizedAccessException());

        await Assert.ThrowsAsync<UnauthorizedAccessException>(
            () => _sut.Login(input)
        );
    }

    [Fact]
    public async Task Login_CorrectUsernameAndPassword_ReturnsToken()
    {
        LoginRequestDTO input = new()
        {
            Username = "username",
            PasswordHash = "password"
        };
        BuildTokenDTO repoResponse = new()
        {
            NameIdentifier = "1",
            Name = "username",
            PasswordHash = "password",
            Role = "Example"
        };
        _mockRepo.Setup(x => x.GetUserByUsernameAsync("username"))
            .ReturnsAsync(repoResponse);
        _mockConfig.Setup(x => x["Jwt:Key"]).Returns(MOCK_KEY);
        _mockConfig.Setup(x => x["Jwt:Issuer"]).Returns(MOCK_ISSUER);
        _mockConfig.Setup(x => x["Jwt:Audience"]).Returns(MOCK_AUDIENCE);

        TokenResponseDTO actual = await _sut.Login(input);
        Assert.NotNull(actual);
    }
}