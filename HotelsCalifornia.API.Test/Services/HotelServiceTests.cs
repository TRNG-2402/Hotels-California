namespace HotelsCalifornia.Test.Services;
using HotelsCalifornia.Services;
using HotelsCalifornia.Data;
using HotelsCalifornia.Models;
using HotelsCalifornia.DTOs;
using Moq;

public class HotelServiceTests
{
    private readonly Mock<IHotelRepository> _mockRepo;
    private readonly HotelService _sut;

    // 33 characters long
    private const string LONG_STRING = "123456789012345678901234567890123";
    // 501 characters long
    private const string VERY_LONG_STRING = "123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901";

    public HotelServiceTests()
    {
        _mockRepo = new();
        _sut = new(_mockRepo.Object);
    }

    // GetHotelsAsync

    [Fact]
    public async Task GetHotelsAsync_ReturnsNotEmpty()
    {
        List<Hotel> repoResponse = [];
        Hotel responseHotel = new()
        {
            Id = 1,
            Name = "Example Inn",
            Description = "This is a hotel",
            Address = "123 Example Dr"
        };
        repoResponse.Add(responseHotel);
        _mockRepo.Setup(x => x.GetHotelsAsync()).ReturnsAsync(repoResponse);

        IEnumerable<OutHotelDTO> serviceResponse = await _sut.GetHotelsAsync();
        Assert.NotEmpty(serviceResponse);
    }

    // GetHotelAsync

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public async Task GetHotelAsync_InvalidId_ThrowsArgumentOutOfRangeException(int id)
    {
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
            () => _sut.GetHotelAsync(id)
        );
    }

    [Fact]
    public async Task GetHotelAsync_UnusedId_ThrowsKeyNotFoundException()
    {
        _mockRepo.Setup(x => x.GetHotelByIdAsync(2)).ThrowsAsync(new KeyNotFoundException());
        await Assert.ThrowsAsync<KeyNotFoundException>(
            () => _sut.GetHotelAsync(2)
        );
    }

    [Fact]
    public async Task GetHotelAsync_ValidId_ReturnsDTO()
    {
        Hotel repoResponse = new()
        {
            Id = 1,
            Name = "Example Inn",
            Description = "This is a hotel",
            Address = "123 Example Dr"
        };
        _mockRepo.Setup(x => x.GetHotelByIdAsync(1)).ReturnsAsync(repoResponse);

        OutHotelDTO expected = new()
        {
            Id = 1,
            Name = "Example Inn",
            Description = "This is a hotel",
            Address = "123 Example Dr"
        };
        OutHotelDTO actual = await _sut.GetHotelAsync(1);

        Assert.NotNull(actual);
        Assert.Equal(expected.Id,           actual.Id);
        Assert.Equal(expected.Name,         actual.Name);
        Assert.Equal(expected.Description,  actual.Description);
        Assert.Equal(expected.Address,      actual.Address);
    }

    // CreateHotelAsync

    [Theory]
    [InlineData("", "123 Example Dr", "This is a hotel")]
    [InlineData(LONG_STRING, "123 Example Dr", "This is a hotel")]
    [InlineData("Example", "", "This is a hotel")]
    [InlineData("Example", "123 Example Dr", VERY_LONG_STRING)]
    public async Task CreateHotelAsync_InvalidParams_ThrowsArgumentException(string name, string address,
        string? description)
    {
        NewHotelDTO input = new()
        {
            Name = name,
            Address = address,
            Description = description
        };
        await Assert.ThrowsAsync<ArgumentException>(
            () => _sut.CreateHotelAsync(input)
        );
    }

    [Theory]
    [InlineData("This is a hotel")]
    [InlineData(null)]
    public async Task CreateHotelAsync_ValidParams_ReturnsDTO(string? description)
    {
        NewHotelDTO input = new()
        {
            Name = "Example",
            Address = "123 Example Dr",
            Description = description
        };
        Hotel repoResponse = new()
        {
            Id = 1,
            Name = "Example",
            Address = "123 Example Dr",
            Description = description
        };
        _mockRepo.Setup(x => x.CreateHotelAsync(input)).ReturnsAsync(repoResponse);

        OutHotelDTO expected = new()
        {
            Id = 1,
            Name = "Example",
            Address = "123 Example Dr",
            Description = description
        };
        OutHotelDTO actual = await _sut.CreateHotelAsync(input);

        Assert.Equal(expected.Id,           actual.Id);
        Assert.Equal(expected.Name,         actual.Name);
        Assert.Equal(expected.Address,      actual.Address);
        Assert.Equal(expected.Description,  actual.Description);
    }

    // UpdateHotelAsync

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public async Task UpdateHotelAsync_InvalidId_ThrowsArgumentOutOfRangeException(int id)
    {
        UpdateHotelDTO input = new()
        {
            Id = id,
            Name = "Example",
            Address = "123 Example Dr",
            Description = "This is a hotel"
        };
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
            () => _sut.UpdateHotelAsync(input)
        );
    }

    [Theory]
    [InlineData(null, null, null)]
    [InlineData(LONG_STRING, null, null)]
    [InlineData("", null, null)]
    [InlineData(null, "", null)]
    [InlineData(null, null, VERY_LONG_STRING)]
    public async Task UpdateHotelAsync_InvalidParams_ThrowsArgumentException(string? name,
        string? address, string? description)
    {
        UpdateHotelDTO input = new()
        {
            Id = 1,
            Name = name,
            Address = address,
            Description = description
        };
        await Assert.ThrowsAsync<ArgumentException>(
            () => _sut.UpdateHotelAsync(input)
        );
    }

    [Fact]
    public async Task UpdateHotelAsync_UnusedId_ThrowsKeyNotFoundException()
    {
        UpdateHotelDTO input = new()
        {
            Id = 2,
            Name = "Example",
            Address = "123 Example Dr"
        };
        _mockRepo.Setup(x => x.UpdateHotelAsync(input)).ThrowsAsync(new KeyNotFoundException());
        await Assert.ThrowsAsync<KeyNotFoundException>(
            () => _sut.UpdateHotelAsync(input)
        );
    }

    [Theory]
    [InlineData("Example Edit", null, null)]
    [InlineData(null, "123 Example Ct", null)]
    [InlineData(null, null, "This is not a motel")]
    public async Task UpdateHotelAsync_ValidParams_Returns(string? name, string? address,
        string? description)
    {
        UpdateHotelDTO input = new()
        {
            Id = 1,
            Name = name,
            Address = address,
            Description = description
        };
        Hotel repoResponse = new()
        {
            Id = 1,
            Name = input.Name ?? "Example",
            Address = input.Address ?? "123 Example St",
            Description = input.Description ?? "This is a hotel"
        };
        _mockRepo.Setup(x => x.UpdateHotelAsync(input)).ReturnsAsync(repoResponse);

        OutHotelDTO expected = new()
        {
            Id = 1,
            Name = input.Name ?? "Example",
            Address = input.Address ?? "123 Example St",
            Description = input.Description ?? "This is a hotel"
        };
        OutHotelDTO actual = await _sut.UpdateHotelAsync(input);

        Assert.Equal(expected.Id,           actual.Id);
        Assert.Equal(expected.Name,         actual.Name);
        Assert.Equal(expected.Address,      actual.Address);
        Assert.Equal(expected.Description,  actual.Description);
    }

    // DeleteHotelAsync

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public async Task DeleteHotelAsync_InvalidId_ThrowsArgumentOutOfRangeException(int id)
    {
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
            () => _sut.DeleteHotelAsync(id)
        );
    }

    [Fact]
    public async Task DeleteHotelAsync_UnusedId_ThrowsKeyNotFoundException()
    {
        _mockRepo.Setup(x => x.DeleteHotelAsync(2)).ThrowsAsync(new KeyNotFoundException());
        await Assert.ThrowsAsync<KeyNotFoundException>(
            () => _sut.DeleteHotelAsync(2)
        );
    }

    [Fact]
    public async Task DeleteHotelAsync_ValidId_Returns()
    {
        Hotel repoResponse = new()
        {
            Id = 1,
            Name = "Example",
            Address = "123 Example Dr",
            Description = "This is a hotel"
        };
        _mockRepo.Setup(x => x.DeleteHotelAsync(1)).ReturnsAsync(repoResponse);
        OutHotelDTO expected = new()
        {
            Id = 1,
            Name = "Example",
            Address = "123 Example Dr",
            Description = "This is a hotel"
        };
        OutHotelDTO actual = await _sut.DeleteHotelAsync(1);

        Assert.Equal(expected.Id,           actual.Id);
        Assert.Equal(expected.Name,         actual.Name);
        Assert.Equal(expected.Address,      actual.Address);
        Assert.Equal(expected.Description,  actual.Description);
    }

}