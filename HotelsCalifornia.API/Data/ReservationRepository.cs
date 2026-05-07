namespace HotelsCalifornia.Data;
using HotelsCalifornia.Models;
using HotelsCalifornia.DTOs;
using HotelsCalifornia.Services;
using Microsoft.EntityFrameworkCore;

public interface IReservationRepository
{
    /// <summary>
    /// Gets all reservations in the database.
    /// </summary>
    Task<IEnumerable<Reservation>> GetReservationsAsync();
    /// <summary>
    /// Gets all reservations in the database fitting a certain hotel.
    /// </summary>
    Task<IEnumerable<Reservation>> GetReservationsByHotelAsync(int hotelId);
    /// <summary>
    /// Gets a reservation by its associated ID
    /// </summary>
    Task<Reservation> GetReservationAsync(int reservationId);
    /// <summary>
    /// Creates a new reservation based on a DTO
    /// </summary>
    Task<Reservation> CreateReservationAsync(NewReservationDTO newRes);
}

public class ReservatioRepository(AppDbContext context) : IReservationRepository
{
    private readonly AppDbContext _context = context;

    public async Task<IEnumerable<Reservation>> GetReservationsAsync()
    {
        return await _context.Reservations.ToListAsync();
    }

    public async Task<IEnumerable<Reservation>> GetReservationsByHotelAsync(int hotelId)
    {
        return await _context.Reservations
            .Where(r => r.HotelId == hotelId).ToListAsync();
    }

    public async Task<Reservation> GetReservationAsync(int reservationId)
    {
        return await _context.Reservations.FindAsync(reservationId)
            ?? throw new KeyNotFoundException($"No reservation with ID {reservationId} in database");
    }

    public async Task<Reservation> CreateReservationAsync(NewReservationDTO newRes)
    {
        Reservation reservation = new()
        {
            RoomId = newRes.RoomId,
            CheckInTime = newRes.CheckInTime,
            DriversLicense = newRes.DriversLicense,
            Email = newRes.Email,
            PhoneNumber = newRes.PhoneNumber
        };
        if (newRes.MemberId != 0)
            reservation.MemberId = newRes.MemberId;
        await _context.Reservations.AddAsync(reservation);
        return reservation;
    }

    public async Task<Reservation> UpdateReservationAsync(UpdateReservationDTO updateRes)
    {
        Reservation res = await GetReservationAsync(updateRes.ReservationId);
        if (updateRes.RoomId != 0 && res.RoomId != updateRes.RoomId)
            res.RoomId = updateRes.RoomId;
        if (updateRes.HotelId != 0 && res.HotelId != updateRes.HotelId)
            res.HotelId = updateRes.HotelId;
        if (updateRes.CheckOutTime != null)
            res.CheckOutTime = updateRes.CheckOutTime;
        if (updateRes.IsCanceled)
            res.IsCanceled = true;
        if (updateRes.DriversLicense != null && res.DriversLicense != updateRes.DriversLicense)
            res.DriversLicense = updateRes.DriversLicense;
        if (updateRes.Email != null && res.Email != updateRes.Email)
            res.Email = updateRes.Email;
        if (updateRes.PhoneNumber != 0 && res.PhoneNumber != updateRes.PhoneNumber)
            res.PhoneNumber = updateRes.PhoneNumber;
        await _context.SaveChangesAsync();
        return res;
    }
}