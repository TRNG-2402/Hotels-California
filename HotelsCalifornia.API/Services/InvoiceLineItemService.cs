using HotelsCalifornia.Models;
using HotelsCalifornia.DTOs;
using HotelsCalifornia.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace HotelsCalifornia.Services;

public interface IInvoiceLineItemService
{
    Task<IEnumerable<InvoiceLineItem>> GetInvoiceLineItemsAsync();

    Task<IEnumerable<InvoiceLineItem>> GetInvoiceLineItemsByInvoiceIdAsync(int invoiceId);

    Task<InvoiceLineItem> GetinvoiceLineItemByIdAsync(int invoiceLineItemId);

    Task<InvoiceLineItem> CreateInvoiceLineItemAsync(NewInvoiceLineItemDTO newInvoiceLineItem);

}

public class InvoiceLineItemService(IInvoiceLineItemRepository repo) : IInvoiceLineItemService
{
    private readonly IInvoiceLineItemRepository _repo = repo;

    public async Task<IEnumerable<InvoiceLineItem>> GetInvoiceLineItemsAsync()
    {
        return await _repo.GetInvoiceLineItemsAsync();
    }

    public async Task<IEnumerable<InvoiceLineItem>> GetInvoiceLineItemsByInvoiceIdAsync(int invoiceId)
    {
        if (invoiceId < 0)
            throw new ArgumentOutOfRangeException("Invoice ID must be a positive number");

        return await _repo.GetInvoiceLineItemsByInvoiceIdAsync(invoiceId);

    }

    public async Task<InvoiceLineItem> GetinvoiceLineItemByIdAsync(int invoiceLineItemId)
    {
        if (invoiceLineItemId < 0)
            throw new ArgumentOutOfRangeException("Invoice Line Item ID must be a positive number");
        return await _repo.GetInvoiceLineItemByIdAsync(invoiceLineItemId);
    }

    public async Task<InvoiceLineItem> CreateInvoiceLineItemAsync(NewInvoiceLineItemDTO newInvoiceLineItem)
    {

        // int must be > 0
        if (newInvoiceLineItem.InvoiceId < 0)
            throw new ArgumentOutOfRangeException("Invoice ID must be positive");
        // double > 0
        if (newInvoiceLineItem.Amount < 0.00)
            throw new ArgumentOutOfRangeException("Amount must be greater than 0");
        // string
        return await _repo.CreateInvoiceLineItemAsync(newInvoiceLineItem);
    }
}