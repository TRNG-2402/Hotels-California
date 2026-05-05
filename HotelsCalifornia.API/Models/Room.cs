/* 
Room: represents a singular room. 
PK: Composite key (Room Number, Hotel ID)
Room Number
Hotel Id (FK)
Daily Rate
Num Beds
Description


*/


using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HotelsCalifornia.Models;

public class Room
{
    [Key]
    public int Id {get;set;}
    [ForeignKey("Hotel")]
    public int HotelId {get;set;}
    [Required]
    public int RoomNumber {get;set;}
    [Required]
    public double DailyRate {get;set;} = 100.00;
    [Required]
    public int NumBeds {get;set;} = 1;
    [MaxLength(200)]
    public String? Description {get;set;}

}