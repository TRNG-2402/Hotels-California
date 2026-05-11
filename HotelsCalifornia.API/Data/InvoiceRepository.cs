namespace HotelsCalifornia.Data;
using HotelsCalifornia.Models;
using Microsoft.EntityFrameworkCore;
using HotelsCalifornia.DTOs;

public interface IInvoiceRepository
{
    Task<IEnumerable<Invoice>> GetInvoicesAsync();
    Task<Invoice> GetInvoiceByIdAsync(int id);
    Task<Invoice> CreateInvoiceAsync(Invoice newInvoice);
    Task<Invoice> UpdateInvoiceAsync(UpdateInvoiceDTO invoiceToUpdate);
    Task<Invoice> DeleteInvoiceAsync(int id);
}

public class InvoiceRepository : IInvoiceRepository
{
    private readonly AppDbContext _context;

    public InvoiceRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Invoice>> GetInvoicesAsync()
    {
        return await _context.Invoices.ToListAsync();
    }

    public async Task<Invoice> GetInvoiceByIdAsync(int id)
    {
        return await _context.Invoices.FindAsync(id)
            ?? throw new KeyNotFoundException($"No invoice with ID ${id}");
    }

    public async Task<Invoice> CreateInvoiceAsync(Invoice newInvoice)
    {
        _context.Invoices.Add(newInvoice);
        await _context.SaveChangesAsync();
        return newInvoice;
    }

    public async Task<Invoice> UpdateInvoiceAsync(UpdateInvoiceDTO updateInvoice)
    {
        Invoice invoiceToUpdate = await GetInvoiceByIdAsync(updateInvoice.Id);
        invoiceToUpdate.IsPaid = updateInvoice.IsPaid;

        await _context.SaveChangesAsync();
        return invoiceToUpdate;
    }

    public async Task<Invoice> DeleteInvoiceAsync(int id)
    {
        Invoice invoiceToDelete = await GetInvoiceByIdAsync(id);
        _context.Invoices.Remove(invoiceToDelete);
        
        await _context.SaveChangesAsync();
        return invoiceToDelete;
    }
}