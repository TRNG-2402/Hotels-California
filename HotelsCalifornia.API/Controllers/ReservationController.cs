namespace HotelsCalifornia.Controllers;
using HotelsCalifornia.Services;
using HotelsCalifornia.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

[Route("api/[controller]")]
[ApiController]
public class ReservationController(IReservationService service) : ControllerBase
{
    private readonly IReservationService _service = service;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<OutReservationDTO>>> GetReservations()
    {
        return Ok(await _service.GetReservationsAsync());
    }

    [HttpGet("/hotel/{hotelId}")]
    public async Task<ActionResult<IEnumerable<OutReservationDTO>>> GetReservationsByHotel(int hotelId)
    {
        return Ok(await _service.GetReservationsByHotelAsync(hotelId));
    }

    [HttpGet("/id/{reservationId}")]
    public async Task<ActionResult<OutReservationDTO>> GetReservation(int reservationId)
    {
        return Ok(await _service.GetReservationAsync(reservationId));
    }

    [HttpPost]
    public async Task<ActionResult> CreateReservation([FromBody] NewReservationDTO newRes)
    {
        OutReservationDTO reservation = await _service.CreateReservationAsync(newRes);
        return Created(nameof(CreateReservation), reservation);
    }

    [HttpPatch]
    [AllowAnonymous]
    public async Task<ActionResult> UpdateReservation([FromBody] UpdateReservationDTO updateRes)
    {
        await _service.UpdateReservationAsync(updateRes);
        return NoContent();
    }
}