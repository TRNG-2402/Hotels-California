namespace HotelsCalifornia.Controllers;

using HotelsCalifornia.Services;
using HotelsCalifornia.Models;
using HotelsCalifornia.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

[Route("api/[controller]")]
[ApiController]
[Authorize(Policy = "AdminOnly")]
public class UserController(IUserService service) : ControllerBase
{
    private readonly IUserService _service = service;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReturnUserDTO>>> GetUsersAsync()
    {
        return Ok(await _service.GetUsersAsync());
    }

    [HttpGet("managers")]
    public async Task<ActionResult<IEnumerable<ReturnManagerDTO>>> GetManagersAsync()
    {
        return Ok(await _service.GetManagersAsync());
    }

    [HttpGet("members")]
    public async Task<ActionResult<IEnumerable<ReturnMemberDTO>>> GetMembersAsync()
    {
        return Ok(await _service.GetMembersAsync());
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin,Manager,Member")]
    public async Task<ActionResult<ReturnUserDTO>> GetUserByIdAsync(int id)
    {
        return Ok(await _service.GetUserAsync(id));
    }

    [HttpPost("Member")]
    [AllowAnonymous]
    public async Task<ActionResult<User>> CreateUserAsync([FromBody] NewMemberDTO newUser)
    {
        User created = await _service.CreateUserAsync(newUser);
        return Created(nameof(CreateUserAsync), created);
    }

    [HttpPost("Manager")]
    [AllowAnonymous]
    public async Task<ActionResult<User>> CreateUserAsync([FromBody] NewManagerDTO newUser)
    {
        Console.WriteLine("HIT CONTROLLER");

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        User created = await _service.CreateUserAsync(newUser);
        return Created(nameof(CreateUserAsync), created);
    }

    [HttpPost("Admin")]
    [AllowAnonymous]
    public async Task<ActionResult<User>> CreateUserAsync([FromBody] NewAdminDTO newUser)
    {
        User created = await _service.CreateUserAsync(newUser);
        return Created(nameof(CreateUserAsync), created);
    }

    [HttpPatch]
    [Authorize(Roles = "Admin,Manager,Member")]
    public async Task<ActionResult> UpdateUserAsync([FromBody] UpdateUserDTO updateUser)
    {
        string? loggedInUserIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (loggedInUserIdClaim is null)
            return Unauthorized();

        int loggedInUserId = int.Parse(loggedInUserIdClaim);
        bool isAdmin = User.IsInRole("Admin");

        if (!isAdmin && updateUser.Id != loggedInUserId)
            return Forbid();

        await _service.UpdateUserAsync(updateUser);
        return NoContent();
    }

    [HttpPatch("{id}/{points}")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<ActionResult> IncrementMemberRewards(int id, int points)
    {
        await _service.IncrementMemberRewards(id, points);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin,Manager,Member")]
    public async Task<ActionResult> DeleteUserAsync(int id)
    {
        await _service.DeleteUserAsync(id);
        return NoContent();
    }
}
