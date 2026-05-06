namespace HotelsCalifornia.Data;
using HotelsCalifornia.Models;
using Microsoft.EntityFrameworkCore;
using HotelsCalifornia.DTOs;

public interface IInvoiceRepo
{
    Task<List<Invoice>> GetInvoiceAsync();
    Task<Invoice> GetInvoiceByIdAsync(int id);
    Task<Invoice> CreateInvoiceAsync(Invoice newInvoice);
    Task<Invoice> UpdateInvoiceAsync(Invoice invoiceToUpdate);
    Task<Invoice> DeleteInvoiceAsync(int id);
}

public class InvoiceRepo(AppDbContext context) : IInvoiceRepo
{
    private readonly AppDbContext _context;

    public InvoiceRepo(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Invoice>> GetInvoiceAsync()
    {
        return await _context.Invoices.ToListAsync();
    }

    public async Task<Invoice> GetInvoiceByIdAsync(int id)
    {
        return await _context.Invoices.FindAsync(id);
    }

    public async Task<Invoice> CreateInvoiceAsync(Invoice newInvoice);
    {
        _context.Invoices.Add(newInvoice);
        await _context.SaveChangesAsync();
        return newInvoice;
    }

    public async Task<Invoice> UpdateInvoiceAsync(Invoice invoiceToUpdate);
    {
        Hotel invoiceToUpdate = await _context.Invoices.FindAsync(invoiceToUpdate.Id);
        await _context.SaveChangesAsync();
        return invoiceToUpdate;
    }

    public async Task<Invoice> DeleteInvoiceAsync(int id);
    {
        Invoice? invoiceToDelete = await _context.Invoices.FindAsync(id);

        if (invoiceToDelete is not null) 
        {
            _context.Invoices.Remove(invoiceToDelete);
            await _context.SaveChangesAsync();
        }

        return invoiceToDelete;
    }
}