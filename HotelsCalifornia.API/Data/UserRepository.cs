namespace HotelsCalifornia.Data;
using HotelsCalifornia.Models;
using HotelsCalifornia.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Routing.Template;
using System.Diagnostics;
using System.Buffers;
using System.Text.Json;

public interface IUserRepository
{
    /// <summary>
    /// Returns a list of all rooms in the database
    /// </summary>
    Task<IEnumerable<User>> GetUsersAsync();

    Task<IEnumerable<Manager>> GetManagersAsync();
    Task<IEnumerable<Member>> GetMembersAsync();

    /// <summary>
    /// Returns a room associated with a given hotel
    /// </summary>
    Task<User> GetUserByIdAsync(int userId);
    /// <summary>
    /// Returns a user associated with a given username
    /// </summary>
    Task<BuildTokenDTO> GetUserByUsernameAsync(string email);
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

    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<IEnumerable<Manager>> GetManagersAsync()
    {
        return await _context.Managers.ToListAsync();
    }

    public async Task<IEnumerable<Member>> GetMembersAsync()
    {
        return await _context.Members.ToListAsync();
    }

    public async Task<User> GetUserByIdAsync(int userId)
    {
        User user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId)
            ?? throw new UnauthorizedAccessException("Invalid username or password");

        return user;
    }

    public async Task<BuildTokenDTO> GetUserByUsernameAsync(string username)
    {
        User user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username)
            ?? throw new KeyNotFoundException($"No user with username {username}");

        return new BuildTokenDTO
        {
            NameIdentifier = user.Id.ToString(),
            Name = user.Username,
            PasswordHash = user.PasswordHash,
            Role = user switch
            {
                Admin a => "Admin",
                Manager m => "Manager",
                Member m => "Member",
                _ => "Unknown"
            },
            HotelId = user is Manager manager ? manager.HotelId : null
        };
    }

    public async Task<User> CreateUserAsync(NewUserDTO dto)
    {
        if (dto.Username is null || dto.PasswordHash is null)
        {
            throw new ArgumentException("Username and password MUST have values");
        }

        var existing = await _context.Users.FirstOrDefaultAsync(u => u.Username == dto.Username);
        if (existing is not null)
            throw new ArgumentException("Username already taken");

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

        Console.WriteLine(JsonSerializer.Serialize(dto));
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User> UpdateUserAsync(UpdateUserDTO updateUserDTO) {
        User toUpdate = await GetUserByIdAsync(updateUserDTO.Id);

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
