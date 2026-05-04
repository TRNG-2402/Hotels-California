/* 
Hotel table: 
Hotel Id (pk)
Name 
Description
Address
*/
using System.ComponentModel.DataAnnotations;
namespace HotelsCalifornia.Models;


public class Hotel
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(32)]
    public String Name { get; set; } = string.Empty;
    [MaxLength(500)]
    public String? Description {get;set;}
    public String? Address {get;set;}

    public List<Room> Rooms {get;set;} = new List<Room>();
}