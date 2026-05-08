
/*  
Member
InBLOCKLIST
Member id (pk)
Membername
PasswordHash
Reservations(List<int> reservIds?)
Driver’s License
Email
Phone number


Manager
Manager Id (pk)
Hotel id
Member ID (fk)

Admin: 
Admin Id (pk)
Member ID (fk)

*/




namespace HotelsCalifornia.DTOs;


public enum UserType
{
    Admin,
    Manager,
    Member
}

public class ReturnUserDTO
{
    public int Id { get; set; }
    public UserType userType {get;set;}
    public required string Username { get; set; }
    public required string PasswordHash { get; set; }
    public int? HotelId { get; set; }
    public string? LicenseNumber { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public int? RewardPoints { get; set; }
    public bool? InBlocklist { get; set; }

}

public class ReturnMemberDTO : ReturnUserDTO
{
    public required string LicenseNumber { get; set; }
    public required string Email { get; set; }
    public string PhoneNumber { get; set; }
    public int RewardPoints { get; set; }
    public bool InBlocklist { get; set; }

}

public class ReturnManagerDTO : ReturnUserDTO
{
    public int HotelId { get; set; }
}

public class ReturnAdminDTO : ReturnUserDTO
{

}