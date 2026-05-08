namespace HotelsCalifornia.DTOs;


public class UpdateUserDTO
{
    
    public int Id { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }
}

public class UpdateMemberDTO : UpdateUserDTO
{    
    public string LicenseNumber { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; } = "1234567890";
    public int RewardPoints { get; set; } = 0;
    public bool InBlocklist { get; set; } = false;
}

public class UpdateManagerDTO : NewUserDTO
{
    // nothing requred here
}

public class UpdateAdminDTO : NewUserDTO
{
    // nothing required here
}