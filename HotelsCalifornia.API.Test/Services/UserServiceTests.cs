namespace HotelsCalifornia.Test.Services;
using HotelsCalifornia.Services;
using HotelsCalifornia.Data;
using HotelsCalifornia.Models;
using HotelsCalifornia.DTOs;
using Moq;

public class UserServiceTests
{
    private readonly Mock<IUserRepository> _mockRepo;
    private readonly Mock<IHotelService> _mockHotelService;
    private readonly UserService _sut;

    private const string VALID_LICENSE = "004611610";
    private const string VALID_PHONE_NUM = "+1 200-867-5309";
    private const string VALID_EMAIL = "someone@example.com";

    public UserServiceTests() {
        _mockRepo = new();
        _mockHotelService = new();
        _sut = new(_mockRepo.Object, _mockHotelService.Object);
    }

    // GetUsersAsync

    [Fact]
    public async Task GetUsersAsync_ReturnsNotEmpty()
    {
        List<User> repoResponse = [];
        Admin response = new()
        {
            Id = 1,
            Username = "example",
            PasswordHash = "password"
        };
        repoResponse.Add(response);
        _mockRepo.Setup(x => x.GetUsersAsync()).ReturnsAsync(repoResponse);

        IEnumerable<ReturnUserDTO> serviceResponse = await _sut.GetUsersAsync();
        Assert.NotEmpty(serviceResponse);
    }

    // GetUserAsync

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public async Task GetUserAsync_InvalidId_ThrowsArgumentOutOfRangeException(int id)
    {
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
            () => _sut.GetUserAsync(id)
        );
    }

    [Fact]
    public async Task GetUserAsync_ValidId_Returns()
    {
        Admin repoResponse = new()
        {
            Id = 1,
            Username = "example",
            PasswordHash = "password"
        };
        _mockRepo.Setup(x => x.GetUserByIdAsync(1)).ReturnsAsync(repoResponse);
        ReturnUserDTO expected = new()
        {
            Id = 1,
            Username = "example",
            userType = UserType.Admin
        };
        ReturnUserDTO actual = await _sut.GetUserAsync(1);
        Assert.Equal(expected.Id, actual.Id);
        Assert.Equal(expected.Username, actual.Username);
        Assert.Equal(expected.userType, actual.userType);
    }

    // CreateUserAsync

    [Theory]
    [InlineData("", "password")]
    [InlineData("123456789012345678901", "password")]
    [InlineData("username", "")]
    [InlineData("username", "123456789012345678901234567890123456789012345678901")]
    public async Task CreateUserAsync_InvalidParamsForUser_ThrowsArgumentException(string username,
        string passwordHash)
    {
        NewUserDTO input = new()
        {
            Username = username,
            PasswordHash = passwordHash
        };
        await Assert.ThrowsAsync<ArgumentException>(
            () => _sut.CreateUserAsync(input)
        );
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public async Task CreateUserAsync_InvalidHotelForManager_ThrowsArgumentOutOfRange(int hotelId)
    {
        NewManagerDTO input = new()
        {
            Username = "manager",
            PasswordHash = "password",
            HotelId = hotelId
        };
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
            () => _sut.CreateUserAsync(input)
        );
    }

    [Theory]
    [InlineData("123456", VALID_EMAIL, VALID_PHONE_NUM)]
    [InlineData("12345678901234567890123456789012", VALID_EMAIL, VALID_PHONE_NUM)]
    [InlineData(VALID_LICENSE, "someone@example", VALID_PHONE_NUM)]
    [InlineData(VALID_LICENSE, "example.com", VALID_PHONE_NUM)]
    [InlineData(VALID_LICENSE, ".com", VALID_PHONE_NUM)]
    [InlineData(VALID_LICENSE, "someone", VALID_PHONE_NUM)]
    [InlineData(VALID_LICENSE, VALID_EMAIL, "1234")]
    [InlineData(VALID_LICENSE, VALID_EMAIL, "phone number")]
    public async Task CreateUserAsync_InvalidIdentificationForMember_ThrowsArgumentException(
        string driversLicense, string email, string phoneNumber)
    {
        NewMemberDTO input = new()
        {
            Username = "member",
            PasswordHash = "password",
            LicenseNumber = driversLicense,
            Email = email,
            PhoneNumber = phoneNumber
        };
        await Assert.ThrowsAsync<ArgumentException>(
            () => _sut.CreateUserAsync(input)
        );
    }

    [Fact]
    public async Task CreateUserAsync_ValidIdentification_Returns()
    {
        NewMemberDTO input = new()
        {
            Username = "member",
            PasswordHash = "password",
            LicenseNumber = VALID_LICENSE,
            Email = VALID_EMAIL,
            PhoneNumber = VALID_PHONE_NUM
        };
        User repoResponse = new Member()
        {
            Id = 1,
            Username = "member",
            PasswordHash = "password",
            LicenseNumber = VALID_LICENSE,
            Email = VALID_EMAIL,
            PhoneNumber = VALID_PHONE_NUM,
            RewardPoints = 0,
            InBlocklist = false
        };
        _mockRepo.Setup(x => x.CreateUserAsync(input)).ReturnsAsync(repoResponse);

        User actual = await _sut.CreateUserAsync(input);
        Assert.True(actual is Member);
        Assert.Equal(repoResponse, actual);
    }

    // UpdateUserAsync

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public async Task UpdateUserAsync_InvalidId_ThrowsArgumentOutOfRangeException(int id)
    {
        UpdateAdminDTO input = new()
        {
            Id = id,
            Username = "username",
            PasswordHash = "password"
        };
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
            () => _sut.UpdateUserAsync(input)
        );
    }

    [Fact]
    public async Task UdateUserAsync_ValidParams_Returns()
    {
        UpdateMemberDTO input = new()
        {
            Id = 1,
            Username = "member",
            PasswordHash = "password",
            LicenseNumber = VALID_LICENSE,
            Email = VALID_EMAIL,
            PhoneNumber = VALID_PHONE_NUM
        };
        User repoResponse = new Member()
        {
            Id = 1,
            Username = "member",
            PasswordHash = "password",
            LicenseNumber = VALID_LICENSE,
            Email = VALID_EMAIL,
            PhoneNumber = VALID_PHONE_NUM,
            RewardPoints = 0,
            InBlocklist = false
        };
        _mockRepo.Setup(x => x.UpdateUserAsync(input)).ReturnsAsync(repoResponse);

        User actual = await _sut.UpdateUserAsync(input);
        Assert.True(actual is Member);
        Assert.Equal(repoResponse, actual);
    }

    // DeleteUserAsync

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public async Task DeleteUserAsync_InvalidId_ThrowsArgumentOutOfRangeException(int id)
    {
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
            () => _sut.DeleteUserAsync(id)
        );
    }

    [Fact]
    public async Task DeleteUserAsync_ValidId_Returns()
    {
        User repoResponse = new Admin()
        {
            Id = 1,
            Username = "username",
            PasswordHash = "password"
        };
        _mockRepo.Setup(x => x.DeleteUserAsync(1)).ReturnsAsync(repoResponse);
    }

}