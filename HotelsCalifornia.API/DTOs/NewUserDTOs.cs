namespace HotelsCalifornia.DTOs;


public class NewUserDTO
{
    public required string Username { get; set; }
    public required string PasswordHash { get; set; }
}

public class NewMemberDTO : NewUserDTO
{    
    public required string LicenseNumber { get; set; }
    public required string Email { get; set; }
    public string PhoneNumber { get; set; }
}

public class NewManagerDTO : NewUserDTO
{
    public int HotelId {get;set;}
}

public class NewAdminDTO : NewUserDTO
{
    // nothing required here
}