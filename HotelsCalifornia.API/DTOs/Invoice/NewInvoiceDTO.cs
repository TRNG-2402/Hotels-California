namespace HotelsCalifornia.DTOs;

public class NewInvoiceDTO
{
    public int MemberId { get; set; }
    public required int ReservationId { get; set; }
    public bool IsPaid { get; set; }
}