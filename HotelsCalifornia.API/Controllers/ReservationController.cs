namespace HotelsCalifornia.Controllers;
using HotelsCalifornia.Services;
using HotelsCalifornia.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ReservationController(IReservationService service) : ControllerBase
{
    private readonly IReservationService _service = service;

    [HttpGet]
    [Authorize(Policy = "AdminOnly")]
    public async Task<ActionResult<IEnumerable<OutReservationDTO>>> GetReservations()
    {
        return Ok(await _service.GetReservationsAsync());
    }

    [HttpGet("reservation/hotel/{hotelId}")]
    [Authorize(Policy = "ManagerOnly")]
    public async Task<ActionResult<IEnumerable<OutReservationDTO>>> GetReservationsByHotel(int hotelId)
    {
        string? managerHotelIdClaim = User.FindFirstValue("hotelId");

        if (managerHotelIdClaim is null)
            return Unauthorized();

        int managerHotelId = int.Parse(managerHotelIdClaim);

        if (hotelId != managerHotelId)
            return Forbid();

        return Ok(await _service.GetReservationsByHotelAsync(hotelId));
    }

    [HttpGet("reservation/member/{memberId}")]
    [Authorize(Roles = "Admin,Manager,Member")]
    public async Task<ActionResult<IEnumerable<OutReservationDTO>>> GetReservationsByMemberId(int memberId)
    {
        string? loggedInUserIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (loggedInUserIdClaim is null)
            return Unauthorized();

        int loggedInUserId = int.Parse(loggedInUserIdClaim);
        bool canViewOtherMembers = User.IsInRole("Admin") || User.IsInRole("Manager");

        if (!canViewOtherMembers && memberId != loggedInUserId)
            return Forbid();

        return Ok(await _service.GetReservationsByMemberIdAsync(memberId));
    }

    [HttpGet("/id/{reservationId}")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<ActionResult<OutReservationDTO>> GetReservation(int reservationId)
    {
        return Ok(await _service.GetReservationAsync(reservationId));
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult> CreateReservation([FromBody] NewReservationDTO newRes)
    {
        OutReservationDTO reservation = await _service.CreateReservationAsync(newRes);
        return Created(nameof(CreateReservation), reservation);
    }

    [HttpPatch]
    [Authorize(Roles = "Admin,Manager,Member")]
    public async Task<ActionResult> UpdateReservation([FromBody] UpdateReservationDTO updateRes)
    {
        await _service.UpdateReservationAsync(updateRes);
        return NoContent();
    }
}
