namespace HotelsCalifornia.DTOs;

public class NewInvoiceDTO
{
    public int MemberId { get; set; }
    public required List<int> ReservationId { get; set; }
    public bool IsPaid { get; set; }
}