namespace HotelsCalifornia.Data;
using HotelsCalifornia.Models;
using HotelsCalifornia.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Routing.Template;
using System.Diagnostics;
using System.Buffers;

public interface IUserRepository
{
    /// <summary>
    /// Returns a list of all rooms in the database
    /// </summary>
    Task<IEnumerable<ReturnUserDTO>> GetUsersAsync();
    /// <summary>
    /// Returns a room associated with a given hotel
    /// </summary>
    Task<User> GetUserByIdAsync(int userId);
    /// <summary>
    /// Creates a new room object
    /// </summary>
    Task<User> CreateUserAsync(NewUserDTO newUser);
    /// <summary>
    /// Updates an existing room object
    /// </summary>
    Task<User> UpdateUserAsync(UpdateUserDTO updateUser);
    /// <summary>
    /// Deletes a room from the database
    /// </summary>
    Task<User> DeleteUserAsync(int UserId);
    Task<Member> IncrementMemberRewards(int id, int points);
}

public class UserRepository(AppDbContext context) : IUserRepository
{
    private readonly AppDbContext _context = context;

    public async Task<IEnumerable<ReturnUserDTO>> GetUsersAsync()
    {

        var users = await _context.Users.ToListAsync();

        return users
            .Select(u => u switch
            {
                Admin a => new ReturnUserDTO
                {
                    Id = a.Id,
                    userType = UserType.Admin,
                    Username = a.Username,
                    PasswordHash = a.PasswordHash
                },
                Manager m => new ReturnUserDTO
                {
                    Id = m.Id,
                    userType = UserType.Manager,
                    Username = m.Username,
                    PasswordHash = m.PasswordHash,
                    HotelId = m.HotelId
                },
                Member mem => new ReturnUserDTO
                {
                    Id = mem.Id,
                    userType = UserType.Member,
                    Username = mem.Username,
                    PasswordHash = mem.PasswordHash,
                    LicenseNumber = mem.LicenseNumber,
                    Email = mem.Email,
                    PhoneNumber = mem.PhoneNumber,
                    RewardPoints = mem.RewardPoints,
                    InBlocklist = mem.InBlocklist
                },
                _ => throw new Exception("Unknown user type")
            })
            .ToList();

    }

    public async Task<User> GetUserByIdAsync(int userId)
    {
        return await _context.Users.FindAsync(userId) ?? throw new KeyNotFoundException(
            $"No user with id {userId} in database"
        );
    }

    public async Task<User> CreateUserAsync(NewUserDTO dto)
    {
        if (dto.Username is null || dto.PasswordHash is null)
        {
            throw new ArgumentException("Username and password MUST have values");
        }
        User user = dto switch
        {
            NewAdminDTO admin => new Admin
            {
                Username = admin.Username,
                PasswordHash = admin.PasswordHash
            },
            NewManagerDTO manager => new Manager
            {
                Username = manager.Username,
                PasswordHash = manager.PasswordHash,
                HotelId = manager.HotelId
            },
            NewMemberDTO member => new Member
            {
                Username = member.Username,
                PasswordHash = member.PasswordHash,
                LicenseNumber = member.LicenseNumber,
                Email = member.Email,
                PhoneNumber = member.PhoneNumber
            },

            _ => throw new ArgumentException("Invalid user type")
        };


        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User> UpdateUserAsync(UpdateUserDTO updateUserDTO) {
        User toUpdate = await GetUserByIdAsync(updateUserDTO.Id);

        if (updateUserDTO.Username is not null)
            toUpdate.Username = updateUserDTO.Username;
        if (updateUserDTO.PasswordHash is not null)
            toUpdate.PasswordHash = updateUserDTO.PasswordHash;

        if (toUpdate is Member member && updateUserDTO is UpdateMemberDTO updateMemberDTO)
        {
            if (updateMemberDTO.LicenseNumber is not null)
                member.LicenseNumber = updateMemberDTO.LicenseNumber;
            if (updateMemberDTO.Email is not null)
                member.Email = updateMemberDTO.Email;
            member.PhoneNumber = updateMemberDTO.PhoneNumber;
            member.RewardPoints = updateMemberDTO.RewardPoints;
            member.InBlocklist = updateMemberDTO.InBlocklist;
        }
        await _context.SaveChangesAsync();
        return toUpdate;
    }

    public async Task<Member> IncrementMemberRewards(int id, int points)
    {
        User user = await GetUserByIdAsync(id);
        if (user is null)
        {
            throw new Exception("User is not found");
        }
        if (user is Member member)
        {
            member.RewardPoints += points;
        } else
        {
            throw new ArgumentException("This user is not a member. Only members can have points");
        }
        return (Member)user;
    }



    public async Task<User> DeleteUserAsync(int userId)
    {
        User toDelete = await GetUserByIdAsync(userId);
        _context.Users.Remove(toDelete);
        await _context.SaveChangesAsync();
        return toDelete;
    }

}