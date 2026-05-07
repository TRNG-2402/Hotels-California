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
        return Ok(await _service.GetRoomsAsync());
    }

    [HttpGet("/id/{roomId}")]
    public async Task<ActionResult<Room>> GetRoom(int roomId)
    {
        return Ok(await _service.GetRoomAsync(roomId));
    }

    [HttpPost]
    public async Task<ActionResult<Room>> CreateRoom([FromBody] NewRoomDTO newRoom)
    {
        Room created = await _service.CreateRoomAsync(newRoom);
        return Created(nameof(CreateRoom), created);
    }

    [HttpPatch]
    public async Task<ActionResult> UpdateRoom([FromBody] UpdateRoomDTO updateRoom)
    {
        await _service.UpdateRoomAsync(updateRoom);
        return NoContent();
    }

    [HttpDelete("/id/{roomId}")]
    public async Task<ActionResult> DeleteRoom(int roomId)
    {
        await _service.DeleteRoomAsync(roomId);
        return NoContent();
    }
}