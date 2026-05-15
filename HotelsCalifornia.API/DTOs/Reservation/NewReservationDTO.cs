namespace HotelsCalifornia.DTOs;

public class NewReservationDTO
{
    public int MemberId { get; set; }
    public int RoomId { get; set; }
    public int HotelId { get; set; }
    public required DateTime CheckInTime { get; set; }
    public required DateTime CheckOutTime { get; set; }
    public required string DriversLicense { get; set; }
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }
}