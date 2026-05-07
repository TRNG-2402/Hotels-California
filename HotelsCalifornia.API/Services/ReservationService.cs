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
    Task<IEnumerable<Reservation>> GetReservationsAsync();
    /// <summary>
    /// Gets reservations by their associated hotel
    /// </summary>
    Task<IEnumerable<Reservation>> GetReservationsByHotel(int hotelId);
    /// <summary>
    /// Gets a specific reservation by its ID
    /// </summary>
    Task<Reservation> GetReservationAsync(int reservationId);
    /// <summary>
    /// Books a new reservation
    /// </summary>
    Task<Reservation> CreateReservationAsync(NewReservationDTO newRes);
    /// <summary>
    /// Updates an existing reservation
    /// </summary>
    Task<Reservation> UpdateReservationAsync(UpdateReservationDTO updateRes);
}

public class ReservationService(IReservationRepository repo) : IReservationService
{
    private readonly IReservationRepository _repo = repo;

    public async Task<IEnumerable<Reservation>> GetReservationsAsync()
    {
        return await _repo.GetReservationsAsync();
    }

    public async Task<IEnumerable<Reservation>> GetReservationsByHotel(int hotelId)
    {
        if (hotelId < 1)
            throw new ArgumentOutOfRangeException("Hotel ID must be a positive number");
        return await _repo.GetReservationsByHotelAsync(hotelId);
    }

    public async Task<Reservation> GetReservationAsync(int reservationId)
    {
        if (reservationId < 1)
            throw new ArgumentOutOfRangeException("Reservation ID must be a positive number");
        return await _repo.GetReservationAsync(reservationId);
    }

    public async Task<Reservation> CreateReservationAsync(NewReservationDTO newRes)
    {
        if (newRes.MemberId < 0)
            throw new ArgumentOutOfRangeException("Member ID must be non-negative");
        if (newRes.RoomId < 1)
            throw new ArgumentOutOfRangeException("Room ID must be a positive number");
        if (!isValidEmail(newRes.Email))
            throw new ArgumentException($"{newRes.Email} is not a valid email");
        if (newRes.PhoneNumber < 1000000000 ||
            newRes.PhoneNumber > 9999999999)
            throw new ArgumentOutOfRangeException("Phone number must be 9 digits");
        if (newRes.DriversLicense.Length < 7 ||
            newRes.DriversLicense.Length > 31) // what is washington's DEAL
            throw new ArgumentException("Invalid drivers license");
        return await _repo.CreateReservationAsync(newRes);
    }

    public async Task<Reservation> UpdateReservationAsync(UpdateReservationDTO updateRes)
    {
        if (updateRes.ReservationId < 1)
            throw new ArgumentOutOfRangeException("Reservation ID must be a positive number");
        if (updateRes.Email is null &&
            updateRes.DriversLicense is null &&
            updateRes.PhoneNumber == 0 &&
            updateRes.CheckOutTime is null)
            throw new ArgumentException("No information has been changed");
        if (updateRes.Email != null &&
            !isValidEmail(updateRes.Email))
            throw new ArgumentException($"{updateRes.Email} is not a valid email");
        if (updateRes.PhoneNumber != 0 &&
            updateRes.PhoneNumber < 1000000000 ||
            updateRes.PhoneNumber > 9999999999)
            throw new ArgumentOutOfRangeException("Phone number must be 9 digits");
        if (updateRes.DriversLicense != null &&
            updateRes.DriversLicense?.Length < 7 ||
            updateRes.DriversLicense?.Length > 31)
            throw new ArgumentException("Invalid drivers license");
        return await _repo.UpdateReservationAsync(updateRes);
    }

    private bool isValidEmail(string email)
    {
        string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
        return Regex.IsMatch(email, pattern);
    }
}