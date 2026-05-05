
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



using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelsCalifornia.Models;


public class User
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(20)]
    public required string Username { get; set; }
    [Required]
    [MaxLength(50)]
    public required string PasswordHash { get; set; }
}

public class Member : User
{
    [Required]
    [Range(9, 14)]
    public required string LicenseNumber { get; set; }
    [Required]
    [MaxLength(50)]
    public required string Email { get; set; }
    [Required]
    public int PhoneNumber { get; set; }
    public int RewardPoints { get; set; } = 0;
    public bool InBlocklist { get; set; } = false;
    public List<Reservation> Reservations { get; set; } = new List<Reservation>();

}

public class Admin : User
{

}