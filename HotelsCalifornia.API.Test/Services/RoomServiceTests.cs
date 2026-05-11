namespace HotelsCalifornia.Test.Services;
using HotelsCalifornia.Services;
using HotelsCalifornia.Data;
using HotelsCalifornia.Models;
using HotelsCalifornia.DTOs;
using Moq;

public class RoomServiceTests
{
    private readonly Mock<IRoomRepository> _mockRepo;
    private readonly RoomService _sut;

    private const string VERY_LONG_STRING = "123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901";

    public RoomServiceTests()
    {
        _mockRepo = new();
        _sut = new(_mockRepo.Object);
    }

    // GetRoomsAsync

    [Fact]
    public async Task GetRoomsAsync_ReturnsAllRooms()
    {
        List<Room> repoResponse = [];
        Room responseRoom = new()
        {
            Id = 1,
            HotelId = 1,
            RoomNumber = 1,
            DailyRate = 100.00,
            NumBeds = 1,
            Description = "This is a room"
        };
        repoResponse.Add(responseRoom);
        _mockRepo.Setup(x => x.GetRoomsAsync()).ReturnsAsync(repoResponse);
        IEnumerable<Room> actual = await _sut.GetRoomsAsync();
        Assert.Equal(repoResponse, actual);
    }

    // GetRoomsByHotelAsync

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public async Task GetRoomsByHotelAsync_InvalidId_ThrowsArgumentOutOfRangeException(int id)
    {
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
            () => _sut.GetRoomsByHotelAsync(id)
        );
    }

    [Fact]
    public async Task GetRoomsByHotelAsync_UnusedId_ThrowsKeyNotFoundException()
    {
        _mockRepo.Setup(x => x.GetRoomsByHotelAsync(2)).ThrowsAsync(new KeyNotFoundException());
        await Assert.ThrowsAsync<KeyNotFoundException>(
            () => _sut.GetRoomsByHotelAsync(2)
        );
    }

    [Fact]
    public async Task GetRoomsByHotelAsync_ValidId_Returns()
    {
        List<Room> repoResponse = [];
        Room responseRoom = new()
        {
            Id = 1,
            HotelId = 1,
            RoomNumber = 1,
            DailyRate = 100.00,
            NumBeds = 1,
            Description = "This is a room"
        };
        repoResponse.Add(responseRoom);
        _mockRepo.Setup(x => x.GetRoomsByHotelAsync(1)).ReturnsAsync(repoResponse);
        IEnumerable<Room> actual = await _sut.GetRoomsByHotelAsync(1);
        Assert.Equal(repoResponse, actual);
    }

    // GetRoomAsync

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public async Task GetRoomAsync_InvalidId_ThrowsArgumentOutOfRangeException(int id)
    {
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
            () => _sut.GetRoomAsync(id)
        );
    }

    [Fact]
    public async Task GetRoomAsync_UnusedId_ThrowsKeyNotFoundException()
    {
        _mockRepo.Setup(x => x.GetRoomByIdAsync(2)).ThrowsAsync(new KeyNotFoundException());
        await Assert.ThrowsAsync<KeyNotFoundException>(
            () => _sut.GetRoomAsync(2)
        );
    }

    [Fact]
    public async Task GetRoomAsync_ValidId_Returns()
    {
        Room repoResponse = new()
        {
            Id = 1,
            HotelId = 1,
            RoomNumber = 1,
            DailyRate = 100.00,
            NumBeds = 1,
            Description = "This is a room"
        };
        _mockRepo.Setup(x => x.GetRoomByIdAsync(1)).ReturnsAsync(repoResponse);
        Room actual = await _sut.GetRoomAsync(1);
        Assert.Equal(repoResponse, actual);
    }

    // CreateRoomAsync

    [Theory]
    [InlineData(-1, 1, 100.00, 1)]
    [InlineData(0, 1, 100.00, 1)]
    [InlineData(1, -1, 100.00, 1)]
    [InlineData(1, 0, 100.00, 1)]
    [InlineData(1, 1, -1, 1)]
    [InlineData(1, 1, 0, 1)]
    [InlineData(1, 1, 100.00, -1)]
    [InlineData(1, 1, 100.00, 0)]
    public async Task CreateRoomAsync_InvalidNumParams_ThrowsArgumentOutOfRangeException(int hotelId,
        int roomNumber, double dailyRate, int numBeds)
    {
        NewRoomDTO input = new()
        {
            HotelId = hotelId,
            RoomNumber = roomNumber,
            DailyRate = dailyRate,
            NumBeds = numBeds
        };
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
            () => _sut.CreateRoomAsync(input)
        );
    }

    [Fact]
    public async Task CreateRoomAsync_TooLongDescription_ThrowsArgumentException()
    {
        NewRoomDTO input = new()
        {
            HotelId = 1,
            RoomNumber = 1,
            DailyRate = 100.00,
            NumBeds = 1,
            Description = VERY_LONG_STRING
        };
        await Assert.ThrowsAsync<ArgumentException>(
            () => _sut.CreateRoomAsync(input)
        );
    }

    [Fact]
    public async Task CreateRoomAsync_ValidParams_Returns()
    {
        NewRoomDTO input = new()
        {
            HotelId = 1,
            RoomNumber = 1,
            DailyRate = 100.00,
            NumBeds = 1
        };
        Room repoResponse = new()
        {
            Id = 1,
            HotelId = 1,
            RoomNumber = 1,
            DailyRate = 100.00,
            NumBeds = 1
        };
        _mockRepo.Setup(x => x.CreateRoomAsync(input)).ReturnsAsync(repoResponse);
        Room actual = await _sut.CreateRoomAsync(input);
        Assert.Equal(repoResponse, actual);
    }

    // UpdateRoomAsync

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public async Task UpdateRoomAsync_InvalidId_ThrowsArgumentOutOfRangeException(int id)
    {
        UpdateRoomDTO input = new()
        {
            Id = id,
            DailyRate = 100.00,
            NumBeds = 1,
            Description = "This is a room"
        };
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
            () => _sut.UpdateRoomAsync(input)
        );
    }

    [Fact]
    public async Task UpdateRoomAsync_EmptyArgs_ThrowsArgumentException()
    {
        UpdateRoomDTO input = new()
        {
            Id = 1
        };
        await Assert.ThrowsAsync<ArgumentException>(
            () => _sut.UpdateRoomAsync(input)
        );
    }

    [Fact]
    public async Task UpdateRoomAsync_TooLongDescription_ThrowsArgumentException()
    {
        UpdateRoomDTO input = new()
        {
            Id = 1,
            Description = VERY_LONG_STRING
        };
        await Assert.ThrowsAsync<ArgumentException>(
            () => _sut.UpdateRoomAsync(input)
        );
    }

    [Theory]
    [InlineData(150.00, 0, null)]
    [InlineData(0, 2, null)]
    [InlineData(0, 0, "This is not a house")]
    public async Task UpdateRoomAsync_ValidParams_Returns(double dailyRate, int numBeds,
        string? description)
    {
        UpdateRoomDTO input = new()
        {
            Id = 1,
            DailyRate = dailyRate,
            NumBeds = numBeds,
            Description = description
        };
        Room repoResponse = new()
        {
            Id = input.Id,
            DailyRate = (dailyRate > 0) ? dailyRate : 100.00,
            NumBeds = (numBeds > 0) ? numBeds : 1,
            Description = description ?? "This is a room",
            HotelId = 1,
            RoomNumber = 1
        };
        _mockRepo.Setup(x => x.UpdateRoomAsync(input)).ReturnsAsync(repoResponse);
        Room actual = await _sut.UpdateRoomAsync(input);
        Assert.Equal(repoResponse, actual);
    }

    // DeleteRoomAsync

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public async Task DeleteRoomAsync_InvalidId_ThrowsArgumentOutOfRangeException(int id)
    {
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
            () => _sut.DeleteRoomAsync(id)
        );
    }

    [Fact]
    public async Task DeleteRoomAsync_UnusedId_ThrowsKeyNotFoundException()
    {
        _mockRepo.Setup(x => x.DeleteRoomAsync(2)).ThrowsAsync(new KeyNotFoundException());
        await Assert.ThrowsAsync<KeyNotFoundException>(
            () => _sut.DeleteRoomAsync(2)
        );
    }

    [Fact]
    public async Task DeleteRoomAsync_ValidId_Returns()
    {
        Room repoResponse = new()
        {
            Id = 1,
            HotelId = 1,
            RoomNumber = 1,
            DailyRate = 100.00,
            NumBeds = 1
        };
        _mockRepo.Setup(x => x.DeleteRoomAsync(1)).ReturnsAsync(repoResponse);
        Room actual = await _sut.DeleteRoomAsync(1);
        Assert.Equal(repoResponse, actual);
    }

}