namespace HotelsCalifornia.Test.Services;
using HotelsCalifornia.Services;
using HotelsCalifornia.Data;
using HotelsCalifornia.Models;
using HotelsCalifornia.DTOs;
using Moq;

public class ReservationServiceTests
{
    private readonly Mock<IReservationRepository> _mockRepo;
    private readonly ReservationService _sut;

    private readonly DateTime CHECK_IN = new DateTime(2000, 1, 1);
    private readonly DateTime CHECK_OUT = new DateTime(2000, 1, 5);
    private const string VALID_LICENSE = "004611610";
    private const string VALID_PHONE_NUM = "(200) 867 5309";
    private const string VALID_EMAIL = "someone@example.com";

    public ReservationServiceTests()
    {
        _mockRepo = new();
        _sut = new(_mockRepo.Object);
    }

    // GetReservationsAsync

    [Fact]
    public async Task GetReservationsAsync_Returns()
    {
        List<Reservation> repoResponse = [];
        Reservation responseReservation = new()
        {
            ReservationId = 1,
            MemberId = 0,
            RoomId = 1,
            HotelId = 1,
            CheckInTime = CHECK_IN,
            CheckOutTime = CHECK_OUT,
            DriversLicense = VALID_LICENSE,
            PhoneNumber = VALID_PHONE_NUM,
            Email = VALID_EMAIL
        };
        repoResponse.Add(responseReservation);
        _mockRepo.Setup(x => x.GetReservationsAsync()).ReturnsAsync(repoResponse);
        IEnumerable<OutReservationDTO> serviceResponse = await _sut.GetReservationsAsync();
        Assert.NotEmpty(serviceResponse);
    }

    // GetReservationsByHotelAsync

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public async Task GetReservationsByHotelAsync_InvalidId_ThrowsArgumentOutOfRangeException(
        int hotelId)
    {
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
            () => _sut.GetReservationsByHotelAsync(hotelId)
        );
    }

    [Fact]
    public async Task GetReservationsByHotelAsync_ValidId_Returns()
    {
        List<Reservation> repoResponse = [];
        Reservation responseReservation = new()
        {
            ReservationId = 1,
            MemberId = 0,
            RoomId = 1,
            HotelId = 1,
            CheckInTime = CHECK_IN,
            CheckOutTime = CHECK_OUT,
            DriversLicense = VALID_LICENSE,
            PhoneNumber = VALID_PHONE_NUM,
            Email = VALID_EMAIL
        };
        repoResponse.Add(responseReservation);
        _mockRepo.Setup(x => x.GetReservationsByHotelAsync(1)).ReturnsAsync(repoResponse);
        IEnumerable<OutReservationDTO> serviceResponse = await _sut.GetReservationsByHotelAsync(1);
        Assert.NotEmpty(serviceResponse);
    }

    // GetReservationAsync

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public async Task GetReservationAsync_InvalidId_ThrowsArgumentOutOfRangeException(int resId)
    {
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
            () => _sut.GetReservationAsync(resId)
        );
    }

    [Fact]
    public async Task GetReservationAsync_ValidId_Returns()
    {
        Reservation repoResponse = new()
        {
            ReservationId = 1,
            MemberId = 0,
            RoomId = 1,
            HotelId = 1,
            CheckInTime = CHECK_IN,
            CheckOutTime = CHECK_OUT,
            DriversLicense = VALID_LICENSE,
            PhoneNumber = VALID_PHONE_NUM,
            Email = VALID_EMAIL
        };
        _mockRepo.Setup(x => x.GetReservationAsync(1)).ReturnsAsync(repoResponse);
        OutReservationDTO expected = new()
        {
            ReservationId = 1,
            MemberId = 0,
            RoomId = 1,
            HotelId = 1,
            CheckInTime = CHECK_IN,
            CheckOutTime = CHECK_OUT,
            DriversLicense = VALID_LICENSE,
            PhoneNumber = VALID_PHONE_NUM,
            Email = VALID_EMAIL
        };
        OutReservationDTO actual = await _sut.GetReservationAsync(1);

        Assert.Equal(expected.ReservationId, actual.ReservationId);
        Assert.Equal(expected.MemberId, actual.MemberId);
        Assert.Equal(expected.RoomId, actual.RoomId);
        Assert.Equal(expected.CheckInTime, actual.CheckInTime);
        Assert.Equal(expected.CheckOutTime, actual.CheckOutTime);
        Assert.Equal(expected.DriversLicense, actual.DriversLicense);
        Assert.Equal(expected.PhoneNumber, actual.PhoneNumber);
        Assert.Equal(expected.Email, actual.Email);
    }

    // CreateReservationAsync

    [Theory]
    [InlineData(-1, 1)]
    [InlineData(1, -1)]
    [InlineData(1, 0)]
    public async Task CreateReservationAsync_InvalidIds_ThrowsArgumentOutOfRangeException(int memberId,
        int roomId)
    {
        NewReservationDTO input = new()
        {
            MemberId = memberId,
            RoomId = roomId,
            CheckInTime = CHECK_IN,
            CheckOutTime = CHECK_OUT,
            DriversLicense = VALID_LICENSE,
            PhoneNumber = VALID_PHONE_NUM,
            Email = VALID_EMAIL
        };
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
            () => _sut.CreateReservationAsync(input)
        );
    }

    [Theory]
    [InlineData("example.com", VALID_PHONE_NUM, VALID_LICENSE)]
    [InlineData("someone@example", VALID_PHONE_NUM, VALID_LICENSE)]
    [InlineData("example", VALID_PHONE_NUM, VALID_LICENSE)]
    [InlineData(".com", VALID_PHONE_NUM, VALID_LICENSE)]
    [InlineData(VALID_EMAIL, "200000000", VALID_LICENSE)]
    [InlineData(VALID_EMAIL, "20000000000", VALID_LICENSE)]
    [InlineData(VALID_EMAIL, VALID_PHONE_NUM, "123456")]
    [InlineData(VALID_EMAIL, VALID_PHONE_NUM, "12345678901234567890123456789012")]
    public async Task CreateReservationAsync_InvalidIdentification_ThrowsArgumentException(string email,
        string phoneNumber, string driversLicense)
    {
        NewReservationDTO input = new()
        {
            MemberId = 0,
            RoomId = 1,
            CheckInTime = CHECK_IN,
            CheckOutTime = CHECK_OUT,
            DriversLicense = driversLicense,
            PhoneNumber = phoneNumber,
            Email = email
        };
        await Assert.ThrowsAsync<ArgumentException>(
            () => _sut.CreateReservationAsync(input)
        );
    }

    [Fact]
    public async Task CreateReservationAsync_CheckInAfterCheckOut_ThrowsArgumentException()
    {
        DateTime checkOut = new(2000, 1, 1);
        NewReservationDTO input1 = new()
        {
            MemberId = 0,
            RoomId = 1,
            CheckInTime = new(2000, 1, 1),
            CheckOutTime = new(2000, 1, 1),
            DriversLicense = VALID_LICENSE,
            PhoneNumber = VALID_PHONE_NUM,
            Email = VALID_EMAIL
        };
        NewReservationDTO input2 = new()
        {
            MemberId = 0,
            RoomId = 1,
            CheckInTime = new(2000, 1, 2),
            CheckOutTime = new(2000, 1, 1),
            DriversLicense = VALID_LICENSE,
            PhoneNumber = VALID_PHONE_NUM,
            Email = VALID_EMAIL
        };
        await Assert.ThrowsAsync<ArgumentException>(
            () => _sut.CreateReservationAsync(input1)
        );
        await Assert.ThrowsAsync<ArgumentException>(
            () => _sut.CreateReservationAsync(input2)
        );
    }

    [Fact]
    public async Task CreateReservationAsync_ValidParams_Returns()
    {
        NewReservationDTO input = new()
        {
            MemberId = 0,
            RoomId = 1,
            CheckInTime = CHECK_IN,
            CheckOutTime = CHECK_OUT,
            DriversLicense = VALID_LICENSE,
            PhoneNumber = VALID_PHONE_NUM,
            Email = VALID_EMAIL
        };
        Reservation repoResponse = new()
        {
            ReservationId = 1,
            MemberId = 0,
            RoomId = 1,
            HotelId = 1,
            CheckInTime = CHECK_IN,
            CheckOutTime = CHECK_OUT,
            DriversLicense = VALID_LICENSE,
            PhoneNumber = VALID_PHONE_NUM,
            Email = VALID_EMAIL
        };
        _mockRepo.Setup(x => x.CreateReservationAsync(input)).ReturnsAsync(repoResponse);

        OutReservationDTO expected = new()
        {
            ReservationId = 1,
            MemberId = 0,
            RoomId = 1,
            HotelId = 1,
            CheckInTime = CHECK_IN,
            CheckOutTime = CHECK_OUT,
            DriversLicense = VALID_LICENSE,
            PhoneNumber = VALID_PHONE_NUM,
            Email = VALID_EMAIL
        };
        OutReservationDTO actual = await _sut.CreateReservationAsync(input);
        Assert.Equal(expected.ReservationId, actual.ReservationId);
        Assert.Equal(expected.MemberId, actual.MemberId);
        Assert.Equal(expected.RoomId, actual.RoomId);
        Assert.Equal(expected.CheckInTime, actual.CheckInTime);
        Assert.Equal(expected.CheckOutTime, actual.CheckOutTime);
        Assert.Equal(expected.DriversLicense, actual.DriversLicense);
        Assert.Equal(expected.PhoneNumber, actual.PhoneNumber);
        Assert.Equal(expected.Email, actual.Email);
    }

    // UpdateReservationAsync

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public async Task UpdateReservationAsync_InvalidReservationId_ThrowsArgumentOutOfRangeException(
        int resId)
    {
        UpdateReservationDTO input = new()
        {
            ReservationId = resId,
            CheckOutTime = null,
            DriversLicense = null,
            Email = null,
            PhoneNumber = null,
            IsCanceled = true
        };
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
            () => _sut.UpdateReservationAsync(input)
        );
    }

    [Theory]
    [InlineData(null, null, null)]
    [InlineData("123456", null, null)]
    [InlineData("12345678901234567890123456789012", null, null)]
    [InlineData(null, "example.com", null)]
    [InlineData(null, "someone@example", null)]
    [InlineData(null, ".com", null)]
    [InlineData(null, "someone", null)]
    [InlineData(null, null, "123456789")]
    [InlineData(null, null, "12345678901")]
    [InlineData(null, null, "(abc) def-ghij")]
    public async Task UpdateReservationAsync_InvalidParams_ThrowsArgumentException(string? driversLicense,
        string? email, string? phoneNumber)
    {
        UpdateReservationDTO input = new()
        {
            ReservationId = 1,
            CheckOutTime = null,
            DriversLicense = driversLicense,
            Email = email,
            PhoneNumber = phoneNumber,
            IsCanceled = false
        };
        await Assert.ThrowsAsync<ArgumentException>(
            () => _sut.UpdateReservationAsync(input)
        );
    }

    [Theory]
    [InlineData(VALID_LICENSE, null, null, false)]
    [InlineData(null, VALID_EMAIL, null, false)]
    [InlineData(null, null, VALID_PHONE_NUM, false)]
    [InlineData(null, null, null, true)]
    public async Task UpdateReservationAsync_ValidParams_Returns(string? driversLicense, string? email,
        string? phoneNumber, bool isCanceled)
    {
        UpdateReservationDTO input = new()
        {
            ReservationId = 1,
            CheckOutTime = null,
            DriversLicense = driversLicense,
            Email = email,
            PhoneNumber = phoneNumber,
            IsCanceled = isCanceled
        };
        Reservation repoResponse = new()
        {
            ReservationId = 1,
            MemberId = 0,
            RoomId = 1,
            HotelId = 1,
            CheckInTime = CHECK_IN,
            CheckOutTime = CHECK_OUT,
            DriversLicense = driversLicense ?? VALID_LICENSE,
            PhoneNumber = phoneNumber ?? VALID_PHONE_NUM,
            Email = email ?? VALID_EMAIL,
            IsCanceled = isCanceled
        };
        _mockRepo.Setup(x => x.UpdateReservationAsync(input)).ReturnsAsync(repoResponse);
        OutReservationDTO expected = new()
        {
            ReservationId = 1,
            MemberId = 0,
            RoomId = 1,
            HotelId = 1,
            CheckInTime = CHECK_IN,
            CheckOutTime = CHECK_OUT,
            DriversLicense = driversLicense ?? VALID_LICENSE,
            PhoneNumber = phoneNumber ?? VALID_PHONE_NUM,
            Email = email ?? VALID_EMAIL,
            IsCanceled = isCanceled
        };
        OutReservationDTO actual = await _sut.UpdateReservationAsync(input);
        Assert.Equal(expected.ReservationId, actual.ReservationId);
        Assert.Equal(expected.MemberId, actual.MemberId);
        Assert.Equal(expected.RoomId, actual.RoomId);
        Assert.Equal(expected.CheckInTime, actual.CheckInTime);
        Assert.Equal(expected.CheckOutTime, actual.CheckOutTime);
        Assert.Equal(expected.DriversLicense, actual.DriversLicense);
        Assert.Equal(expected.PhoneNumber, actual.PhoneNumber);
        Assert.Equal(expected.Email, actual.Email);
        Assert.Equal(expected.IsCanceled, actual.IsCanceled);
    }
    
}