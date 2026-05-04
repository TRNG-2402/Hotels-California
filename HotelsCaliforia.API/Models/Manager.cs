/*
Manager Id (pk)
Hotel id
Member ID (fk)

*/
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelsCalifornia.Models;

public class Manager
{
    [Key]
    public int Id { get; set; }
    // just assume the manager has no hotel
    [ForeignKey("Hotel")]
    public int? HotelId { get; set; }

    [ForeignKey("Member")]
    public int MemberId { get; set; }
}