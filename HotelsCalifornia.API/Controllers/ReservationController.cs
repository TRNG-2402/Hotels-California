namespace HotelsCalifornia.Controllers;
using HotelsCalifornia.Services;
using HotelsCalifornia.Models;
using HotelsCalifornia.DTOs;
using Microsoft.AspNetCore.Mvc;

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
        await _service.CreateReservationAsync(newRes);
        return NoContent();
    }

    [HttpPatch]
    public async Task<ActionResult> UpdateReservation([FromBody] UpdateReservationDTO updateRes)
    {
        await _service.UpdateReservationAsync(updateRes);
        return NoContent();
    }
}