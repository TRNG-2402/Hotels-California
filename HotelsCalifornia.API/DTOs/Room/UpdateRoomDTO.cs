namespace HotelsCalifornia.DTOs;

public class UpdateRoomDTO
{
    public int Id { get; set; }
    public int RoomNumber { get; set; }
    public double DailyRate { get; set; }
    public int NumBeds { get; set; }
    public string? Description { get; set; }
}