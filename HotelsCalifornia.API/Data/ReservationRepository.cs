namespace HotelsCalifornia.Data;

using HotelsCalifornia.Models;
using HotelsCalifornia.DTOs;
using HotelsCalifornia.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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
    /// Gets all reservations in the database fitting a certain member.
    /// </summary>
    Task<IEnumerable<Reservation>> GetReservationsByMemberIdAsync(int memberId);
    /// <summary>
    /// Gets a reservation by its associated ID
    /// </summary>
    Task<Reservation> GetReservationAsync(int reservationId);
    /// <summary>
    /// Creates a new reservation based on a DTO
    /// </summary>
    Task<Reservation> CreateReservationAsync(NewReservationDTO newRes);
    /// <summary>
    /// Updates an existing reservation based on a DTO
    /// </summary>
    Task<Reservation> UpdateReservationAsync(UpdateReservationDTO updateRes);
}

public class ReservationRepository(AppDbContext context) : IReservationRepository
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

    public async Task<IEnumerable<Reservation>> GetReservationsByMemberIdAsync(int memberId)
    {
        return await _context.Reservations
            .Where(r => r.MemberId == memberId).ToListAsync();
    }

    public async Task<Reservation> GetReservationAsync(int reservationId)
    {
        return await _context.Reservations.FindAsync(reservationId)
            ?? throw new KeyNotFoundException($"No reservation with ID {reservationId} in database");
    }

    public async Task<Reservation> CreateReservationAsync(NewReservationDTO newRes)
    {
        if (await isFree(newRes.RoomId, newRes.CheckInTime, newRes.CheckOutTime) == false)
            throw new ArgumentException("Time slot is already booked by another party");
        Reservation reservation = new()
        {
            RoomId = newRes.RoomId,
            CheckInTime = newRes.CheckInTime,
            CheckOutTime = newRes.CheckOutTime,
            DriversLicense = newRes.DriversLicense,
            Email = newRes.Email,
            PhoneNumber = newRes.PhoneNumber
        };
        if (newRes.MemberId != 0)
            reservation.MemberId = newRes.MemberId;
        if (newRes.HotelId != 0)
            reservation.HotelId = newRes.HotelId;
        await _context.Reservations.AddAsync(reservation);
        await _context.SaveChangesAsync();
        return reservation;
    }

    public async Task<Reservation> UpdateReservationAsync(UpdateReservationDTO updateRes)
    {
        Reservation res = await GetReservationAsync(updateRes.ReservationId);
        if (updateRes.IsCanceled)
            res.IsCanceled = true;
        if (updateRes.DriversLicense != null && res.DriversLicense != updateRes.DriversLicense)
            res.DriversLicense = updateRes.DriversLicense;
        if (updateRes.Email != null && res.Email != updateRes.Email)
            res.Email = updateRes.Email;
        if (updateRes.PhoneNumber != null && res.PhoneNumber != updateRes.PhoneNumber)
            res.PhoneNumber = updateRes.PhoneNumber;
        await _context.SaveChangesAsync();
        return res;
    }

    private async Task<bool> isFree(int roomId, DateTime checkIn, DateTime checkOut)
    {
        var conflicting = await _context.Reservations
            .Where(r => r.IsCanceled == false
                && r.RoomId == roomId
                && ((r.CheckInTime < checkIn && r.CheckOutTime > checkIn)
                    || (r.CheckInTime > checkIn && r.CheckInTime < checkOut)))
            .ToListAsync();

        return conflicting.IsNullOrEmpty();
    }
}
