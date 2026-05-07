namespace HotelsCalifornia.Controllers;
using HotelsCalifornia.DTOs;
using HotelsCalifornia.Models;
using HotelsCalifornia.Services;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class RoomController(IRoomService service) : ControllerBase
{
    private readonly IRoomService _service = service;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Room>>> GetAllRooms()
    {
        // Anonymous
        return Ok(await _service.GetRoomsAsync());
    }

    [HttpGet("/hotel/{hotelId}")]
    public async Task<ActionResult<IEnumerable<Room>>> GetRoomsByHotel(int hotelId)
    {
        // Anonymous
        return Ok(await _service.GetRoomsByHotelAsync(hotelId));
    }

    [HttpGet("/id/{roomId}")]
    public async Task<ActionResult<Room>> GetRoom(int roomId)
    {
        // Anonymous
        return Ok(await _service.GetRoomAsync(roomId));
    }

    [HttpPost]
    public async Task<ActionResult<Room>> CreateRoom([FromBody] NewRoomDTO newRoom)
    {
        // Manager only
        Room created = await _service.CreateRoomAsync(newRoom);
        return Created(nameof(CreateRoom), created);
    }

    [HttpPatch]
    public async Task<ActionResult> UpdateRoom([FromBody] UpdateRoomDTO updateRoom)
    {
        // Manager only
        await _service.UpdateRoomAsync(updateRoom);
        return NoContent();
    }

    [HttpDelete("/id/{roomId}")]
    public async Task<ActionResult> DeleteRoom(int roomId)
    {
        // Manager only
        await _service.DeleteRoomAsync(roomId);
        return NoContent();
    }
}