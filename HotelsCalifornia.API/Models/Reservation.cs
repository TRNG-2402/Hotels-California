namespace HotelsCalifornia.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Reservation
{
    [Key]
    public int ReservationId { get; set; }

    [ForeignKey("Member")]
    public int MemberId { get; set; }

    [ForeignKey("Room")]
    [Required]
    public int RoomId { get; set; }

    [ForeignKey("Hotel")]
    [Required]
    public int HotelId { get; set; }

    [Required]
    public required DateTime CheckInTime { get; set; }

    [Required]
    public required DateTime CheckOutTime { get; set; }

    [Required]
    public required string DriversLicense { get; set; }

    [Required]
    public required string Email { get; set; }

    [Required]
    public required string PhoneNumber { get; set; }

    public bool IsCanceled { get; set; } = false;


    public Member? Member { get; set; }
    [Required]
    public Room Room { get; set; } = null!;

}