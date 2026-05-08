namespace HotelsCalifornia.DTOs;

public class UpdateInvoiceLineItemDTO
{
    public int Id { get; set; }

    public double? Amount { get; set; }

    public string? Description { get; set; }
}