namespace HotelsCalifornia.DTOs;

public class NewRoomDTO
{
    public int HotelId { get; set; }
    public int RoomNumber { get; set; }
    public double DailyRate { get; set; }
    public int NumBeds { get; set; }
    public string? Description { get; set; }
}