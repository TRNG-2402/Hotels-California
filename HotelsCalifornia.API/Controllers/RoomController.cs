namespace HotelsCalifornia.Controllers;
using HotelsCalifornia.DTOs;
using HotelsCalifornia.Models;
using HotelsCalifornia.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
[Authorize(Policy = "ManagerOnly")]
public class RoomController(IRoomService service) : ControllerBase
{
    private readonly IRoomService _service = service;

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<Room>>> GetAllRooms()
    {
        return Ok(await _service.GetRoomsAsync());
    }

    [HttpGet("/hotel/{hotelId}")]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<Room>>> GetRoomsByHotel(int hotelId)
    {
        return Ok(await _service.GetRoomsByHotelAsync(hotelId));
    }

    [HttpGet("/id/{roomId}")]
    [AllowAnonymous]
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