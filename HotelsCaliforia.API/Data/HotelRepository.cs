namespace HotelsCalifornia.Data;
using HotelsCalifornia.Models;
using Microsoft.EntityFrameworkCore;
using HotelsCalifornia.DTOs;

public interface IHotelRepository
{
    /// <summary>
    /// Returns all hotels in the database
    /// </summary>
    Task<IEnumerable<Hotel>> GetHotelsAsync();
    /// <summary>
    /// Returns a hotel with the ID matching the input
    /// </summary>
    Task<Hotel> GetHotelByIdAsync(int hotelId);
    /// <summary>
    /// Creates and returns a new instance of a hotel
    /// </summary>
    Task<Hotel> CreateHotelAsync(NewHotelDTO newHotel);
    /// <summary>
    /// Updates an existing hotel
    /// </summary>
    /// <returns>The updated hotel, should the ID attach to a hotel which exists</returns>
    Task<Hotel> UpdateHotelAsync(UpdateHotelDTO updateHotel);
    /// <summary>
    /// Deletes a hotel from the database, if it exists.
    /// </summary>
    /// <returns>The deleted hotel object if it existed, null otherwise</returns>
    Task<Hotel> DeleteHotelAsync(int hotelId);
}

public class HotelRepository(AppDbContext context) : IHotelRepository
{
    private readonly AppDbContext _context = context;

    public async Task<IEnumerable<Hotel>> GetHotelsAsync()
    {
        return await _context.Hotels.ToListAsync();
    }

    public async Task<Hotel> GetHotelByIdAsync(int hotelId)
    {
        return await _context.Hotels.FindAsync(hotelId) ?? throw new KeyNotFoundException(
            $"No hotel found with ID {hotelId}"
        );
    }

    public async Task<Hotel> CreateHotelAsync(NewHotelDTO newHotel)
    {
        Hotel hotel = new()
        {
            Name = newHotel.Name,
            Description = newHotel.Description,
            Address = newHotel.Address
        };
        await _context.Hotels.AddAsync(hotel);
        await _context.SaveChangesAsync();
        return hotel;
    }

    public async Task<Hotel> UpdateHotelAsync(UpdateHotelDTO updateHotel)
    {
        Hotel toUpdate = await GetHotelByIdAsync(updateHotel.Id);
        if (updateHotel.Name is not null)
            toUpdate.Name = updateHotel.Name;
        if (updateHotel.Description is not null)
            toUpdate.Description = updateHotel.Description;
        if (updateHotel.Address is not null)
            toUpdate.Address = updateHotel.Address;
        await _context.SaveChangesAsync();
        return toUpdate;
    }

    public async Task<Hotel> DeleteHotelAsync(int hotelId)
    {
        Hotel toDelete = await GetHotelByIdAsync(hotelId);
        _context.Hotels.Remove(toDelete);
        await _context.SaveChangesAsync();
        return toDelete;
    }
}