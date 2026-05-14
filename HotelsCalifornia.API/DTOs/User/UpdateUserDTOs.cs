namespace HotelsCalifornia.DTOs;


public class UpdateUserDTO
{
    
    public int Id { get; set; }
    public required string Username { get; set; }
    public required string PasswordHash { get; set; }
}

public class UpdateMemberDTO : UpdateUserDTO
{    
    public required string LicenseNumber { get; set; }
    public required string Email { get; set; }
    public string PhoneNumber { get; set; } = "1234567890";
    public int RewardPoints { get; set; } = 0;
    public bool InBlocklist { get; set; } = false;
}

public class UpdateManagerDTO : UpdateUserDTO
{
    // nothing requred here
}

public class UpdateAdminDTO : UpdateUserDTO
{
    // nothing required here
}