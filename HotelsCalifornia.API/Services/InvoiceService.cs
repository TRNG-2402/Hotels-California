namespace HotelsCalifornia.Services;

using HotelsCalifornia.Data;
using HotelsCalifornia.DTOs;
using HotelsCalifornia.Models;

public interface IInvoiceService
{
    Task<IEnumerable<Invoice>> GetInvoicesAsync();
    Task<IEnumerable<Invoice>> GetInvoicesByMemberIdAsync(int id);
    Task<Invoice> GetInvoiceAsync(int id);
    Task<Invoice> CreateInvoiceAsync(NewInvoiceDTO newInvoice);
    Task<Invoice> UpdateInvoiceAsync(UpdateInvoiceDTO invoiceToAdd);
    Task<Invoice> DeleteInvoiceAsync(int id);
}

public class InvoiceService : IInvoiceService
{
    private readonly IInvoiceRepository _repo;

    public InvoiceService(IInvoiceRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<Invoice>> GetInvoicesAsync()
    {
        return await _repo.GetInvoicesAsync();
    }

    public async Task<IEnumerable<Invoice>> GetInvoicesByMemberIdAsync(int memberId)
    {
        if (memberId < 0)
            throw new ArgumentOutOfRangeException("Member ID must be a positive number");

        return await _repo.GetInvoicesByMemberIdAsync(memberId);


    }

    public async Task<Invoice> GetInvoiceAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentOutOfRangeException("Invoice id must be a positive number");

        Invoice? result = await _repo.GetInvoiceByIdAsync(id);

        if (result is null)
            throw new ArgumentException("Invoice does not exist.");

        return result;
    }

    public async Task<Invoice> CreateInvoiceAsync(NewInvoiceDTO newInvoice)
    {
        if (newInvoice.MemberId < 1)
            throw new ArgumentException("Member Id must be greater than zero.");

        if (newInvoice.ReservationId < 1)
            throw new ArgumentException("ReservationId must be greater than zero");

        Invoice invoice = new Invoice();

        invoice.MemberId = newInvoice.MemberId;
        invoice.ReservationId = newInvoice.ReservationId;
        invoice.IsPaid = newInvoice.IsPaid;

        return await _repo.CreateInvoiceAsync(invoice);
    }

    public async Task<Invoice> UpdateInvoiceAsync(UpdateInvoiceDTO updateInvoice)
    {
        if (updateInvoice.Id < 1)
            throw new ArgumentOutOfRangeException("Room ID must be a positive number");

        return await _repo.UpdateInvoiceAsync(updateInvoice);
    }

    public async Task<Invoice> DeleteInvoiceAsync(int id)
    {
        if (id < 1)
            throw new ArgumentOutOfRangeException("Invoice ID must be a positive number");

        return await _repo.DeleteInvoiceAsync(id);
    }
}