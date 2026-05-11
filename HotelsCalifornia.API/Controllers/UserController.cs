namespace HotelsCalifornia.Controllers;
using HotelsCalifornia.Services;
using HotelsCalifornia.Models;
using HotelsCalifornia.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;

[Route("api/[controller]")]
[ApiController]
public class UserController(IUserService service) : ControllerBase
{
    private readonly IUserService _service = service;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReturnUserDTO>>> GetUsersAsync()
    {
        return Ok(await _service.GetUsersAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ReturnUserDTO>> GetUserByIdAsync(int id)
    {
        return Ok(await _service.GetUserAsync(id));
    }

    [HttpPost("Member")]
    public async Task<ActionResult<User>> CreateUserAsync([FromBody] NewMemberDTO newUser)
    {
        User created = await _service.CreateUserAsync(newUser);
        return Created(nameof(CreateUserAsync), created);
    }
    [HttpPost("Manager")]
    public async Task<ActionResult<User>> CreateUserAsync([FromBody] NewManagerDTO newUser)
    {
        User created = await _service.CreateUserAsync(newUser);
        return Created(nameof(CreateUserAsync), created);
    }
    [HttpPost("Admin")]
    public async Task<ActionResult<User>> CreateUserAsync([FromBody] NewAdminDTO newUser)
    {
        User created = await _service.CreateUserAsync(newUser);
        return Created(nameof(CreateUserAsync), created);
    }

    [HttpPatch]
    public async Task<ActionResult> UpdateUserAsync([FromBody] UpdateUserDTO updateUser)
    {
        await _service.UpdateUserAsync(updateUser);
        return NoContent();
    }

    [HttpPatch("{id}/{points}")]
    public async Task<ActionResult> IncrementMemberRewards(int id, int points)
    {
        await _service.IncrementMemberRewards(id, points);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUserAsync(int id)
    {
        await _service.DeleteUserAsync(id);
        return NoContent();
    }
}
