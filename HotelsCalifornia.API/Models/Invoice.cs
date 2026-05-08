/*
Invoice ID (PK)
Member ID (FK, optional)
Reservation ID (FK, one fee to many reservations)
Is Paid?

*/

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelsCalifornia.Models;

public class Invoice
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Member")]
    public int? MemberId { get; set; }
    
    [ForeignKey("Reservation")]
    public List<int> ReservationId { get; set; } = new();

    public bool IsPaid { get; set; } = false;
}