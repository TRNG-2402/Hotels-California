using HotelsCalifornia.Services;
using HotelsCalifornia.Models;
using HotelsCalifornia.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace HotelsCalifornia.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InvoiceLineItemController(IInvoiceLineItemService service) : ControllerBase
{
    private readonly IInvoiceLineItemService _service = service;


    //Get all Invoice Line items
    [HttpGet]
    public async Task<ActionResult<IEnumerable<InvoiceLineItem>>> GetInvoiceLineItemAsync()
    {
        return Ok(await _service.GetInvoiceLineItemsAsync());
    }


    //Get Invoice Line Item By Invoice Id
    [HttpGet("{invoiceId}")]
    public async Task<ActionResult<IEnumerable<InvoiceLineItem>>> GetInvoiceLineItemByInvoiceIdAsync(int invoiceId)
    {
        return Ok(await _service.GetInvoiceLineItemsByInvoiceIdAsync(invoiceId));
    }

    //Get Invoice Line Item By ID
    [HttpGet("{invoiceId}/{id}")]
    public async Task<ActionResult<InvoiceLineItem>> GetInvoiceLineItemByIdAsync(int invoiceId, int id)
    {
        return Ok(await _service.GetinvoiceLineItemByIdAsync(id));
    }


    //Create invoice line item
    [HttpPost]

    public async Task<ActionResult<InvoiceLineItem>> CreateInvoiceLineItemAsync([FromBody] NewInvoiceLineItemDTO newInvoiceLineItem)
    {
        InvoiceLineItem created = await _service.CreateInvoiceLineItemAsync(newInvoiceLineItem);
        return Created(nameof(CreateInvoiceLineItemAsync), created);
    }
}

