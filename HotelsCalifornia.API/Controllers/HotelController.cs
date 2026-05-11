namespace HotelsCalifornia.Controllers;
using HotelsCalifornia.Services;
using HotelsCalifornia.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

[Route("api/[controller]")]
[ApiController]
[Authorize(Policy = "AdminOnly")]
public class HotelController(IHotelService service) : ControllerBase
{
    private readonly IHotelService _service = service;

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<OutHotelDTO>>> GetHotelsAsync()
    {
        return Ok(await _service.GetHotelsAsync());
    }

    [HttpGet("id/{id}")]
    [AllowAnonymous]
    public async Task<ActionResult<OutHotelDTO>> GetHotelByIdAsync(int id)
    {
        return Ok(await _service.GetHotelAsync(id));
    }

    [HttpPost]
    public async Task<ActionResult<OutHotelDTO>> CreateHotelAsync([FromBody] NewHotelDTO newHotel)
    {
        OutHotelDTO created = await _service.CreateHotelAsync(newHotel);
        return Created(nameof(CreateHotelAsync), created);
    }

    [HttpPatch]
    public async Task<ActionResult> UpdateHotelAsync([FromBody] UpdateHotelDTO updateHotel)
    {
        await _service.UpdateHotelAsync(updateHotel);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteHotelAsync(int id)
    {
        await _service.DeleteHotelAsync(id);
        return NoContent();
    }
}
