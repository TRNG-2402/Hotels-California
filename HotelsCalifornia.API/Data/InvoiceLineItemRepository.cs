namespace HotelsCalifornia.Data;

using HotelsCalifornia.Models;
using Microsoft.EntityFrameworkCore;
using HotelsCalifornia.DTOs;

public interface IInvoiceLineItemRepository
{
    Task<IEnumerable<InvoiceLineItem>> GetInvoiceLineItemsAsync();

    Task<InvoiceLineItem> GetInvoiceLineItemByIdAsync(int id);

    Task<InvoiceLineItem> CreateInvoiceLineItemAsync(NewInvoiceLineItemDTO newInvoiceLineItem);

    Task<InvoiceLineItem> UpdateInvoiceLineItemAsync(UpdateInvoiceLineItemDTO updateInvoiceLineItem);

    Task<InvoiceLineItem> DeleteInvoiceLineItemAsync(int id);
}

public class InvoiceLineItemRepository(AppDbContext context) : IInvoiceLineItemRepository
{
    private readonly AppDbContext _context = context;

    public async Task<IEnumerable<InvoiceLineItem>> GetInvoiceLineItemsAsync()
    {
        return await _context.InvoiceLineItems.ToListAsync();
    }

    public async Task<InvoiceLineItem> GetInvoiceLineItemByIdAsync(int id)
    {
        return await _context.InvoiceLineItems.FindAsync(id) ?? throw new KeyNotFoundException($"No Invoice Line Item with ID {id}");
    }

    public async Task<InvoiceLineItem> CreateInvoiceLineItemAsync(NewInvoiceLineItemDTO newInvoiceLineItem)
    {
        InvoiceLineItem invoiceLineItem = new()
        {
            InvoiceId = newInvoiceLineItem.InvoiceId,
            Amount = newInvoiceLineItem.Amount,
            Description = newInvoiceLineItem.Description
        };
        await _context.InvoiceLineItems.AddAsync(invoiceLineItem);
        await _context.SaveChangesAsync();
        return invoiceLineItem;
    }

    public async Task<InvoiceLineItem> UpdateInvoiceLineItemAsync(UpdateInvoiceLineItemDTO updateInvoiceLineItem)
    {
        InvoiceLineItem updatedInvoiceLineItem = await GetInvoiceLineItemByIdAsync(updateInvoiceLineItem.Id);
        if (updateInvoiceLineItem.Amount is not null)
            updatedInvoiceLineItem.Amount = (double)updateInvoiceLineItem.Amount;
        if (updateInvoiceLineItem.Description is not null)
            updatedInvoiceLineItem.Description = updateInvoiceLineItem.Description;
        await _context.SaveChangesAsync();
        return updatedInvoiceLineItem;

    }

    public async Task<InvoiceLineItem> DeleteInvoiceLineItemAsync(int id)
    {
        InvoiceLineItem deletedInvoiceLineItem = await GetInvoiceLineItemByIdAsync(id);
        _context.InvoiceLineItems.Remove(deletedInvoiceLineItem);
        await _context.SaveChangesAsync();
        return deletedInvoiceLineItem;
    }

}