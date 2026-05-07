namespace HotelsCalifornia.DTOs;

public class OutReservationDTO
{
    public int ReservationId { get; set; }
    public int MemberId { get; set; }
    public int RoomId { get; set; }
    public int HotelId { get; set; }
    public required DateTime CheckInTime { get; set; }
    public DateTime? CheckOutTime { get; set; }
    public required string DriversLicense { get; set; }
    public required string Email { get; set; }
    public long PhoneNumber { get; set; }
    public bool IsCanceled { get; set; }
}