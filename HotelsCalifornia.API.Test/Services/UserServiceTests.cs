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

    public UserServiceTests() {
        _mockRepo = new();
        _mockHotelService = new();
        _sut = new(_mockRepo.Object, _mockHotelService.Object);
    }

    // GetUsersAsync

    [Fact]
    public async Task GetUsersAsync_ReturnsNotEmpty()
    {
        List<ReturnUserDTO> repoResponse = [];
        ReturnUserDTO responseDTO = new()
        {
            Id = 1,
            Username = "example",
            userType = UserType.Admin
        };
        repoResponse.Add(responseDTO);
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

    // CreateUserAsync

    // UpdateUserAsync

    // IncrementUserAwards

    // DeleteUserAsync
}