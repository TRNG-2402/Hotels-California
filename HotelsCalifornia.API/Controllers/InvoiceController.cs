namespace HotelsCalifornia.Controllers;

using HotelsCalifornia.Services;
using HotelsCalifornia.Models;
using HotelsCalifornia.DTOs;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class InvoiceController : ControllerBase
{
    private readonly IInvoiceService _service;

    public InvoiceController(IInvoiceService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Invoice>>> GetInvoicesAsync()
    {
        return Ok(await _service.GetInvoicesAsync());
    }
    [HttpGet("Members/{id}")]
    public async Task<ActionResult<IEnumerable<Invoice>>> GetInvoicesByMemberIdAsync(int id)
    {
        return Ok(await _service.GetInvoicesByMemberIdAsync(id));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Invoice>> GetInvoiceByIdAsync(int id)
    {
        return Ok(await _service.GetInvoiceAsync(id));
    }

    [HttpPost]
    public async Task<ActionResult<Invoice>> CreateInvoiceAsync([FromBody] NewInvoiceDTO newInvoice)
    {
        Invoice created = await _service.CreateInvoiceAsync(newInvoice);
        return Created(nameof(CreateInvoiceAsync), created);
    }

    [HttpPatch]
    public async Task<ActionResult> UpdateInvoiceAsync([FromBody] UpdateInvoiceDTO updateInvoice)
    {
        await _service.UpdateInvoiceAsync(updateInvoice);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteInvoiceAsync(int id)
    {
        await _service.DeleteInvoiceAsync(id);
        return NoContent();
    }
}
