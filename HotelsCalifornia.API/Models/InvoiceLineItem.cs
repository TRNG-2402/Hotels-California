/*
Invoice Line Item ID (PK)
Invoice ID (FK)
Amount
Description

*/

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelsCalifornia.Models;

public class InvoiceLineItem
{
    [Key]
    public int Id { get; set; }
    [ForeignKey("Invoice")]
    public int InvoiceId { get; set; }
    [Required]
    public double Amount { get; set; }
    [MaxLength(200)]
    public string? Description { get; set; }
}