namespace HotelsCalifornia.Services;
using HotelsCalifornia.Data;
using HotelsCalifornia.Models;
using HotelsCalifornia.DTOs;

public interface IRoomService
{
    /// <summary>
    /// Returns all the rooms in the database
    /// </summary>
    Task<IEnumerable<Room>> GetRoomsAsync();
    /// <summary>
    /// Returns all the rooms attributed to a certain hotel
    /// </summary>
    Task<IEnumerable<Room>> GetRoomsByHotelAsync(int hotelId);
    /// <summary>
    /// Returns a room from the database based on the entered ID
    /// </summary>
    Task<Room> GetRoomAsync(int roomId);
    /// <summary>
    /// Creates a new room based on a given DTO
    /// </summary>
    Task<Room> CreateRoomAsync(NewRoomDTO newRoom);
    /// <summary>
    /// Updates an existing room
    /// </summary>
    Task<Room> UpdateRoomAsync(UpdateRoomDTO updateRoom);
    /// <summary>
    /// Deletes a room from the database
    /// </summary>
    Task<Room> DeleteRoomAsync(int roomId);
}

public class RoomService(IRoomRepository repo) : IRoomService
{
    private readonly IRoomRepository _repo = repo;

    public async Task<IEnumerable<Room>> GetRoomsAsync()
    {
        return await _repo.GetRoomsAsync();
    }

    public async Task<IEnumerable<Room>> GetRoomsByHotelAsync(int hotelId)
    {
        if (hotelId < 1)
            throw new ArgumentOutOfRangeException("Hotel ID must be a positive number");
        return await _repo.GetRoomsByHotelAsync(hotelId);
    }

    public async Task<Room> GetRoomAsync(int roomId)
    {
        if (roomId < 1)
            throw new ArgumentOutOfRangeException("Room ID must be a positive number");
        return await _repo.GetRoomByIdAsync(roomId);
    }

    public async Task<Room> CreateRoomAsync(NewRoomDTO newRoom)
    {
        if (newRoom.HotelId < 1)
            throw new ArgumentOutOfRangeException("Hotel ID must be a positive number");
        if (newRoom.RoomNumber < 1)
            throw new ArgumentOutOfRangeException("Room Number must be a positive number");
        if (newRoom.DailyRate <= 0)
            throw new ArgumentOutOfRangeException("Daily Rate must be a positive number");
        if (newRoom.NumBeds < 1)
            throw new ArgumentOutOfRangeException("Number of beds must be a positive number");
        if (newRoom.Description?.Length > 500)
            throw new ArgumentException("Description cannot exceed 500 characters");
        return await _repo.CreateRoomAsync(newRoom);
    }

    public async Task<Room> UpdateRoomAsync(UpdateRoomDTO updateRoom)
    {
        if (updateRoom.Id < 1)
            throw new ArgumentOutOfRangeException("Room ID must be a positive number");
        if (updateRoom.DailyRate <= 0 ||
            updateRoom.NumBeds < 1 ||
            updateRoom.Description is null)
            throw new ArgumentException("No information inserted to update room");
        if (updateRoom.Description?.Length > 500)
            throw new ArgumentException("Description cannot exceed 500 characters");
        return await _repo.UpdateRoomAsync(updateRoom);
    }

    public async Task<Room> DeleteRoomAsync(int roomId)
    {
        if (roomId < 1)
            throw new ArgumentOutOfRangeException("Room ID must be a positive number");
        return await _repo.DeleteRoomAsync(roomId);
    }

}