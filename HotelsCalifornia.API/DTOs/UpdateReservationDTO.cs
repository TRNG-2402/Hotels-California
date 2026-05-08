namespace HotelsCalifornia.DTOs;

public class UpdateReservationDTO
{
    public int ReservationId { get; set; }
    public DateTime? CheckOutTime { get; set; }
    public string? DriversLicense { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public bool IsCanceled { get; set; }
}