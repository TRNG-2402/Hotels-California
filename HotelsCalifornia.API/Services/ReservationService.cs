namespace HotelsCalifornia.Services;
using HotelsCalifornia.Models;
using HotelsCalifornia.DTOs;
using HotelsCalifornia.Data;
using System.Text.RegularExpressions;

public interface IReservationService
{
    /// <summary>
    /// Gets all reservations in the database
    /// </summary>
    Task<IEnumerable<OutReservationDTO>> GetReservationsAsync();
    /// <summary>
    /// Gets reservations by their associated hotel
    /// </summary>
    Task<IEnumerable<OutReservationDTO>> GetReservationsByHotelAsync(int hotelId);
    /// <summary>
    /// Gets a specific reservation by its ID
    /// </summary>
    Task<OutReservationDTO> GetReservationAsync(int reservationId);
    /// <summary>
    /// Books a new reservation
    /// </summary>
    Task<OutReservationDTO> CreateReservationAsync(NewReservationDTO newRes);
    /// <summary>
    /// Updates an existing reservation
    /// </summary>
    Task<OutReservationDTO> UpdateReservationAsync(UpdateReservationDTO updateRes);
}

public class ReservationService(IReservationRepository repo) : IReservationService
{
    private readonly IReservationRepository _repo = repo;

    public async Task<IEnumerable<OutReservationDTO>> GetReservationsAsync()
    {
        var reservations = await _repo.GetReservationsAsync();
        List<OutReservationDTO> output = [];
        foreach (var r in reservations)
            _ = output.Append(toDTO(r));
        return output;
        
    }

    public async Task<IEnumerable<OutReservationDTO>> GetReservationsByHotelAsync(int hotelId)
    {
        if (hotelId < 1)
            throw new ArgumentOutOfRangeException("Hotel ID must be a positive number");
        var reservations = await _repo.GetReservationsByHotelAsync(hotelId);
        List<OutReservationDTO> output = [];
        foreach (var r in reservations)
            _ = output.Append(toDTO(r));
        return output;
    }

    public async Task<OutReservationDTO> GetReservationAsync(int reservationId)
    {
        if (reservationId < 1)
            throw new ArgumentOutOfRangeException("Reservation ID must be a positive number");
        return toDTO(await _repo.GetReservationAsync(reservationId));
    }

    public async Task<OutReservationDTO> CreateReservationAsync(NewReservationDTO newRes)
    {
        if (newRes.MemberId < 0)
            throw new ArgumentOutOfRangeException("Member ID must be non-negative");
        if (newRes.RoomId < 1)
            throw new ArgumentOutOfRangeException("Room ID must be a positive number");
        if (!isValidEmail(newRes.Email))
            throw new ArgumentException($"{newRes.Email} is not a valid email");
        if (!isValidPhoneNumber(newRes.PhoneNumber))
            throw new ArgumentOutOfRangeException("Invalid phone number");
        if (newRes.DriversLicense.Length < 7 ||
            newRes.DriversLicense.Length > 31) // what is washington's DEAL
            throw new ArgumentException("Invalid drivers license");
        return toDTO(await _repo.CreateReservationAsync(newRes));
    }

    public async Task<OutReservationDTO> UpdateReservationAsync(UpdateReservationDTO updateRes)
    {
        if (updateRes.ReservationId < 1)
            throw new ArgumentOutOfRangeException("Reservation ID must be a positive number");
        if (updateRes.Email is null &&
            updateRes.DriversLicense is null &&
            updateRes.PhoneNumber is null &&
            updateRes.CheckOutTime is null)
            throw new ArgumentException("No information has been changed");
        if (updateRes.Email != null &&
            !isValidEmail(updateRes.Email))
            throw new ArgumentException($"{updateRes.Email} is not a valid email");
        if (updateRes.PhoneNumber != null &&
            !isValidPhoneNumber(updateRes.PhoneNumber))
            throw new ArgumentOutOfRangeException("Invalid phone number");
        if (updateRes.DriversLicense != null &&
            updateRes.DriversLicense?.Length < 7 ||
            updateRes.DriversLicense?.Length > 31)
            throw new ArgumentException("Invalid drivers license");
        return toDTO(await _repo.UpdateReservationAsync(updateRes));
    }

    private bool isValidEmail(string email)
    {
        string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
        return Regex.IsMatch(email, pattern);
    }

    private bool isValidPhoneNumber(string phoneNumber)
    {
        string pattern = @"^(?:\(?)(\d{3})(?:[\).\s]?)(\d{3})(?:[-\.\s]?)(\d{4})(?!\d)";
        return Regex.IsMatch(phoneNumber, pattern);
    }

    private OutReservationDTO toDTO(Reservation r)
    {
        return new()
        {
            ReservationId = r.ReservationId,
            MemberId = r.MemberId,
            RoomId = r.RoomId,
            HotelId = r.HotelId,
            CheckInTime = r.CheckInTime,
            CheckOutTime = r.CheckOutTime,
            DriversLicense = r.DriversLicense,
            Email = r.Email,
            PhoneNumber = r.PhoneNumber,
            IsCanceled = r.IsCanceled
        };
    }
}