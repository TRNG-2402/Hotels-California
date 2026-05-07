namespace HotelsCalifornia.Services;
using HotelsCalifornia.Data;
using HotelsCalifornia.DTOs;
using HotelsCalifornia.Models;

public interface IHotelService
{
    /// <summary>
    /// Returns all the hotels in the database
    /// </summary>
    Task<IEnumerable<OutHotelDTO>> GetHotelsAsync();
    /// <summary>
    /// Retrieves the hotel with the given ID
    /// </summary>
    Task<OutHotelDTO> GetHotelAsync(int hotelId);
    /// <summary>
    /// Creates a new hotel given a DTO
    /// </summary>
    Task<OutHotelDTO> CreateHotelAsync(NewHotelDTO newHotel);
    /// <summary>
    /// Updates the information for a hotel by its given ID
    /// </summary>
    Task<OutHotelDTO> UpdateHotelAsync(UpdateHotelDTO updateHotel);
    /// <summary>
    /// Deletes a hotel from the database
    /// </summary>
    Task<OutHotelDTO> DeleteHotelAsync(int hotelId);
}

public class HotelService(IHotelRepository repo) : IHotelService
{
    private readonly IHotelRepository _repo = repo;

    public async Task<IEnumerable<OutHotelDTO>> GetHotelsAsync()
    {
        var hotels = await _repo.GetHotelsAsync();
        List<OutHotelDTO> output = [];
        foreach (var h in hotels)
            _ = output.Append(toDTO(h));
        return output;
    }

    public async Task<OutHotelDTO> GetHotelAsync(int hotelId)
    {
        if (hotelId < 1)
            throw new ArgumentOutOfRangeException("Hotel ID must be a positive number");
        return toDTO(await _repo.GetHotelByIdAsync(hotelId));
    }

    public async Task<OutHotelDTO> CreateHotelAsync(NewHotelDTO newHotel)
    {
        if (newHotel.Name.Length < 1)
            throw new ArgumentException("Hotel name cannot be empty");
        if (newHotel.Name.Length > 32)
            throw new ArgumentException("Hotel name must be less than 32 characters");
        if (newHotel.Address.Length < 1)
            throw new ArgumentException("Hotel address cannot be empty");
        if (newHotel.Description?.Length > 500)
            throw new ArgumentException("Hotel description cannot exceed 500 characters");
        return toDTO(await _repo.CreateHotelAsync(newHotel));
    }

    public async Task<OutHotelDTO> UpdateHotelAsync(UpdateHotelDTO updateHotel)
    {
        if (updateHotel.Id < 1)
            throw new ArgumentOutOfRangeException("Hotel Id must be a positive number");
        if (updateHotel.Name is null &&
            updateHotel.Address is null &&
            updateHotel.Description is null)
            throw new ArgumentException("No information inserted to update hotel");
        if (updateHotel.Name?.Length > 32)
            throw new ArgumentException("Hotel name must be less than 32 characters");
        if (updateHotel.Description?.Length > 500)
            throw new ArgumentException("Hotel description cannot exceed 500 characters");
        return toDTO(await _repo.UpdateHotelAsync(updateHotel));
    }

    public async Task<OutHotelDTO> DeleteHotelAsync(int hotelId)
    {
        if (hotelId < 1)
            throw new ArgumentOutOfRangeException("Hotel ID must be a positive number");
        return toDTO(await _repo.DeleteHotelAsync(hotelId));
    }

    private OutHotelDTO toDTO(Hotel hotel)
    {
        return new()
        {
            Id = hotel.Id,
            Name = hotel.Name,
            Address = hotel.Address,
            Description = hotel.Description
        };
    }
}