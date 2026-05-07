namespace HotelsCalifornia.Data;
using HotelsCalifornia.Models;
using HotelsCalifornia.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Routing.Template;

public interface IRoomRepository
{
    /// <summary>
    /// Returns a list of all rooms in the database
    /// </summary>
    Task<IEnumerable<Room>> GetRoomsAsync();
    /// <summary>
    /// Returns a room associated with a given hotel
    /// </summary>
    Task<Room> GetRoomByIdAsync(int roomId);
    /// <summary>
    /// Creates a new room object
    /// </summary>
    Task<Room> CreateRoomAsync(NewRoomDTO newRoom);
    /// <summary>
    /// Updates an existing room object
    /// </summary>
    Task<Room> UpdateRoomAsync(UpdateRoomDTO updateRoom);
    /// <summary>
    /// Deletes a room from the database
    /// </summary>
    Task<Room> DeleteRoomAsync(int roomId);
}

public class RoomRepository(AppDbContext context) : IRoomRepository
{
    private readonly AppDbContext _context = context;

    public async Task<IEnumerable<Room>> GetRoomsAsync()
    {
        return await _context.Rooms.ToListAsync();
    }

    public async Task<Room> GetRoomByIdAsync(int roomId)
    {
        return await _context.Rooms.FindAsync(roomId) ?? throw new KeyNotFoundException(
            $"No room with id {roomId} in database"
        );
    }

    public async Task<Room> CreateRoomAsync(NewRoomDTO newRoom)
    {
        Room room = new()
        {
            HotelId = newRoom.HotelId,
            RoomNumber = newRoom.RoomNumber,
            DailyRate = newRoom.DailyRate,
            NumBeds = newRoom.NumBeds,
            Description = newRoom.Description
        };
        await _context.Rooms.AddAsync(room);
        await _context.SaveChangesAsync();
        return room;
    }

    public async Task<Room> UpdateRoomAsync(UpdateRoomDTO updateRoom) {
        Room toUpdate = await GetRoomByIdAsync(updateRoom.Id);
        if (updateRoom.DailyRate > 0)
            toUpdate.DailyRate = updateRoom.DailyRate;
        if (updateRoom.NumBeds > 0)
            toUpdate.NumBeds = updateRoom.NumBeds;
        if (updateRoom.Description is not null)
            toUpdate.Description = updateRoom.Description;
        await _context.SaveChangesAsync();
        return toUpdate;
    }

    public async Task<Room> DeleteRoomAsync(int roomId)
    {
        Room toDelete = await GetRoomByIdAsync(roomId);
        _context.Rooms.Remove(toDelete);
        await _context.SaveChangesAsync();
        return toDelete;
    }

}