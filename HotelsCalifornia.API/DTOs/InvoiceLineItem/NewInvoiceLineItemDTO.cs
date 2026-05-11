namespace HotelsCalifornia.DTOs;

public class NewInvoiceLineItemDTO
{
    public required int InvoiceId { get; set; }

    public double Amount { get; set; }

    public string? Description { get; set; }
}