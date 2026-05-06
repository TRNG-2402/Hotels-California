namespace HotelsCalifornia.Services;
using HotelsCalifornia.Data;
using HotelsCalifornia.DTOs;
using HotelsCalifornia.Models;

public interface IInvoiceService
{
    Task<IEnumerable<Invoice>> GetInvoicesAsync();
    Task<Invoice> GetInvoiceAsync(int id);
    Task<Invoice> CreateInvoiceAsync(NewHotelDTO newInvoice);
    Task<Invoice> UpdateinvoiceAsync(UpdateHotelDTO invoiceToAdd);
    Task<Invoice> DeleteInvoiceAsync(int id);
}