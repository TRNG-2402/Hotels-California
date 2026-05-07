namespace HotelsCalifornia.DTOs;

public class UpdateReservationDTO
{
    public int ReservationId { get; set; }
    public int RoomId { get; set; }
    public int HotelId { get; set; }
    public DateTime? CheckOutTime { get; set; }
    public string? DriversLicense { get; set; }
    public string? Email { get; set; }
    public int PhoneNumber { get; set; }
    public bool IsCanceled { get; set; }
}