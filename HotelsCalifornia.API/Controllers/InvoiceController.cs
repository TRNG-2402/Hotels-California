namespace HotelsCalifornia.Controllers;
using HotelsCalifornia.Services;
using HotelsCalifornia.Models;
using HotelsCalifornia.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;

[Route("api/[controller]")]
[ApiController]
public class InvoiceController(IInvoiceService service) : ControllerBase
{
    private readonly IControllerService _service;

    public InvoiceController(IControllerService service)
    {
        _service = service;
    }

    
}
