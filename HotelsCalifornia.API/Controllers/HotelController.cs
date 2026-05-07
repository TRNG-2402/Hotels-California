namespace HotelsCalifornia.Controllers;
using HotelsCalifornia.Services;
using HotelsCalifornia.Models;
using HotelsCalifornia.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;

[Route("api/[controller]")]
[ApiController]
public class HotelController(IHotelService service) : ControllerBase
{
    private readonly IHotelService _service = service;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Hotel>>> GetHotelsAsync()
    {
        // Anonymous
        return Ok(await _service.GetHotelsAsync());
    }

    [HttpGet("id/{id}")]
    public async Task<ActionResult<Hotel>> GetHotelByIdAsync(int id)
    {
        // Anonymous
        return Ok(await _service.GetHotelAsync(id));
    }

    [HttpPost]
    public async Task<ActionResult<Hotel>> CreateHotelAsync([FromBody] NewHotelDTO newHotel)
    {
        // Admin only
        Hotel created = await _service.CreateHotelAsync(newHotel);
        return Created(nameof(CreateHotelAsync), created);
    }

    [HttpPatch]
    public async Task<ActionResult> UpdateHotelAsync([FromBody] UpdateHotelDTO updateHotel)
    {
        // Admin only
        await _service.UpdateHotelAsync(updateHotel);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteHotelAsync(int id)
    {
        // Admin only
        await _service.DeleteHotelAsync(id);
        return NoContent();
    }
}
