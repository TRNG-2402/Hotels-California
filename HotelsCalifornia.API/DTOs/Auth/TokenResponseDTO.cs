namespace HotelsCalifornia.DTOs;

public class TokenResponseDTO
{
    public required string Token { get; set; }
    public required DateTime ExpiresAt { get; set; }
}