using HotelsCalifornia.Models;
using HotelsCalifornia.DTOs;
using HotelsCalifornia.Data;

namespace HotelsCalifornia.Services;

public interface IInvoiceLineItemService
{
    Task<IEnumerable<InvoiceLineItem>> GetInvoiceLineItemsAsync();

    Task<IEnumerable<InvoiceLineItem>> GetInvoiceLineItemsByInvoiceIdAsync(int invoiceId);


}