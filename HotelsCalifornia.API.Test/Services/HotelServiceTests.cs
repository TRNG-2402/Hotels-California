namespace HotelsCalifornia.Test.Services;
using HotelsCalifornia.Services;
using HotelsCalifornia.Data;
using HotelsCalifornia.Models;
using HotelsCalifornia.DTOs;
using Moq;
using System.Data.Common;
using System.ComponentModel;
using System.Reflection;

public class HotelServiceTests
{
    private readonly Mock<IHotelRepository> _mockRepo;
    private readonly HotelService _sut;
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

    // CreateHotelAsync

    // UpdateHotelAsync

    // DeleteHotelAsync
}