namespace HotelsCalifornia.DTOs;

public class BuildTokenDTO
{
    public required string NameIdentifier { get; set; }
    public required string Name { get; set; }
    public required string PasswordHash { get; set; }
    public required string Role { get; set; }
    public int? HotelId { get; set; }
}
