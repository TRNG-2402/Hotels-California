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

    public ReservationServiceTests()
    {
        _mockRepo = new();
        _sut = new(_mockRepo.Object);
    }

    
}