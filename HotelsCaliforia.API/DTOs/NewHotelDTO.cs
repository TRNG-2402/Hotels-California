namespace HotelsCalifornia.DTOs;

public class NewHotelDTO
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required string Address { get; set; }
}