namespace HotelsCalifornia.Data;

using HotelsCalifornia.Models;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    internal DbSet<Hotel> Hotels { get; set; }
    internal DbSet<Room> Rooms { get; set; }
    internal DbSet<User> Users { get; set; }
    internal DbSet<Member> Members { get; set; }
    internal DbSet<Manager> Managers { get; set; }
    internal DbSet<Admin> Admins { get; set; }
    internal DbSet<Invoice> Invoices { get; set; }
    internal DbSet<InvoiceLineItem> InvoiceLineItems { get; set; }
    internal DbSet<Reservation> Reservations { get; set; }
    
    public AppDbContext() : base() {}

    public AppDbContext(DbContextOptions options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Component key for room
        modelBuilder.Entity<Room>( entity =>
            entity.HasKey(r => new {r.RoomNumber, r.HotelId})
        );
        modelBuilder.Entity<User>()
            .HasDiscriminator<string>("User_type")
            .HasValue<Member>("Member")
            .HasValue<Manager>("Manager")
            .HasValue<Admin>("Admin");
    }
}